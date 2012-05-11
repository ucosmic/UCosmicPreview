using System;
using System.Linq.Expressions;
using FluentValidation;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class SendSignUpMessageCommand
    {
        public string EmailAddress { get; set; }
        public string SendFromUrl { get; set; }
        public Guid ConfirmationToken { get; internal set; }
    }

    public class SendSignUpMessageHandler : IHandleCommands<SendSignUpMessageCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHandleCommands<SendConfirmEmailMessageCommand> _sendHandler;

        public SendSignUpMessageHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
            , IUnitOfWork unitOfWork
            , IHandleCommands<SendConfirmEmailMessageCommand> sendHandler
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _unitOfWork = unitOfWork;
            _sendHandler = sendHandler;
        }

        public void Handle(SendSignUpMessageCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the establishment
            var establishment = _queryProcessor.Execute(
                new GetEstablishmentByEmailQuery
                {
                    Email = command.EmailAddress,
                    EagerLoad = new Expression<Func<Establishment, object>>[]
                    {
                        e => e.Type.Category,
                    },
                }
            );

            // get the person
            var person = _queryProcessor.Execute(
                new GetPersonByEmailQuery
                {
                    Email = command.EmailAddress,
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Emails,
                        p => p.Affiliations,
                    },
                }
            );

            // create the person if they don't yet exist
            if (person == null)
            {
                var createPersonHandler = new CreatePersonHandler(_entities);
                var createPersonCommand = new CreatePersonCommand
                {
                    DisplayName = command.EmailAddress,
                };
                createPersonHandler.Handle(createPersonCommand);
                person = createPersonCommand.CreatedPerson;
            }

            // affiliate person with establishment
            person.AffiliateWith(establishment);

            // add email address if necessary
            if (person.GetEmail(command.EmailAddress) == null)
                person.AddEmail(command.EmailAddress);

            // save changes so nested command can find the correct data
            _unitOfWork.SaveChanges();

            // send the message
            var sendCommand = new SendConfirmEmailMessageCommand
            {
                EmailAddress = command.EmailAddress,
                Intent = EmailConfirmationIntent.SignUp,
                SendFromUrl = command.SendFromUrl,
            };
            _sendHandler.Handle(sendCommand);
            command.ConfirmationToken = sendCommand.ConfirmationToken;
        }
    }

    public class SendSignUpMessageValidator : AbstractValidator<SendSignUpMessageCommand>
    {
        public SendSignUpMessageValidator(IProcessQueries queryProcessor, ISignMembers memberSigner)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            Establishment establishment = null;
            var loadEstablishment = new Expression<Func<Establishment, object>>[]
            {
                e => e.SamlSignOn,
            };

            Person person = null;
            var loadPerson = new Expression<Func<Person, object>>[]
            {
                p => p.Emails,
                p => p.User
            };

            RuleFor(p => p.EmailAddress)
                // email address cannot be empty
                .NotEmpty()
                    .WithMessage(ValidateEmailAddress.FailedBecauseValueWasEmpty)
                // must be valid against email address regular expression
                .EmailAddress()
                    .WithMessage(ValidateEmailAddress.FailedBecauseValueWasNotValidEmailAddress)
                // the email address must match a non-saml establishment
                .Must(p => ValidateEstablishment.EmailMatchesEntity(p, queryProcessor, loadEstablishment, out establishment))
                    .WithMessage(ValidateEstablishment.FailedBecauseEmailMatchedNoEntity,
                        p => p.EmailAddress)
                // the email address must match a member establishment
                .Must(p => ValidateEstablishment.EmailMatchesEntity(p, queryProcessor, loadEstablishment, out establishment))
                    .WithMessage(ValidateEstablishment.FailedBecauseEmailMatchedNoEntity,
                        p => p.EmailAddress)
                // the email address may match a person
                .Must(p =>
                      {
                          ValidateEmailAddress.ValueMatchesPerson(p, queryProcessor, loadPerson, out person);
                          return true;
                      })
            ;

            // when establishment is not null,
            When(p => establishment != null, () =>
                RuleFor(p => p.EmailAddress)
                    // it must not have saml sign on
                    .Must(p => !establishment.HasSamlSignOn())
                        .WithMessage(ValidateEstablishment.FailedBecauseEstablishmentHasSamlSignOn,
                            p => establishment.RevisionId)
                    // it must be a member
                    .Must(p => establishment.IsMember)
                        .WithMessage(ValidateEstablishment.FailedBecauseEstablishmentIsNotMember,
                            p => person.User.Name)
            );

            // when person is not null,
            When(p => person != null, () =>
                RuleFor(p => p.EmailAddress)
                    // it must not have a registered user
                    .Must(p => person.User == null || !person.User.IsRegistered)
                        .WithMessage(ValidatePerson.FailedBecauseUserIsRegistered,
                            p => person.DisplayName)
                    // it must not have a local member account
                    .Must(p => person.User == null || !memberSigner.IsSignedUp(person.User.Name))
                        .WithMessage(ValidateUser.FailedBecauseNameMatchedLocalMember,
                            p => person.User.Name)
            );
        }
    }
}
