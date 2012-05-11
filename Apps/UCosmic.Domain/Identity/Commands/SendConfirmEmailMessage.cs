using System;
using System.Linq.Expressions;
using FluentValidation;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class SendConfirmEmailMessageCommand
    {
        public string EmailAddress { get; set; }
        public EmailConfirmationIntent Intent { get; set; }
        public string SendFromUrl { get; set; }

        internal EmailTemplateName TemplateName
        {
            get
            {
                switch (Intent)
                {
                    case EmailConfirmationIntent.ResetPassword:
                        return EmailTemplateName.ResetPasswordConfirmation;

                    case EmailConfirmationIntent.CreatePassword:
                        return EmailTemplateName.CreatePasswordConfirmation;
                }
                throw new NotSupportedException(string.Format(
                    "Email confirmation intent '{0}' is not supported.",
                        Intent));
            }
        }
        public Guid ConfirmationToken { get; internal set; }
    }

    public class SendConfirmEmailMessageHandler : IHandleCommands<SendConfirmEmailMessageCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly IHandleCommands<SendEmailMessageCommand> _sendHandler;

        public SendConfirmEmailMessageHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
            , IHandleCommands<SendEmailMessageCommand> sendHandler
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _sendHandler = sendHandler;
        }

        public void Handle(SendConfirmEmailMessageCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the person
            var person = _queryProcessor.Execute(
                new GetPersonByEmailQuery
                {
                    Email = command.EmailAddress,
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Emails,
                    },
                }
            );

            // get the email
            var email = person.GetEmail(command.EmailAddress);

            // create the confirmation
            var secretCode = _queryProcessor.Execute(
                new GenerateRandomSecretQuery(12));
            var confirmation = new EmailConfirmation(command.Intent)
            {
                EmailAddress = email,
                SecretCode = secretCode,
            };
            command.ConfirmationToken = confirmation.Token;
            _entities.Create(confirmation);

            // get the email template
            var template = _queryProcessor.Execute(
                new GetEmailTemplateByNameQuery
                {
                    Name = command.TemplateName.AsSentenceFragment(),
                }
            );

            // create the message
            var message = _queryProcessor.Execute(
                new ComposeEmailMessageQuery(template, email)
                {
                    Formatters = _queryProcessor.Execute(
                        new GetConfirmEmailFormattersQuery(confirmation, command.SendFromUrl)
                    )
                }
            );
            _entities.Create(message);

            // send the message
            _sendHandler.Handle(
                new SendEmailMessageCommand
                {
                    PersonId = message.ToPerson.RevisionId,
                    MessageNumber = message.Number,
                }
            );
        }
    }

    public class SendConfirmEmailMessageValidator : AbstractValidator<SendConfirmEmailMessageCommand>
    {
        public SendConfirmEmailMessageValidator(IProcessQueries queryProcessor, IStorePasswords passwords)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            Person person = null;
            var loadPerson = new Expression<Func<Person, object>>[]
            {
                p => p.Emails,
                p => p.User
            };

            Establishment establishment = null;
            var loadEstablishment = new Expression<Func<Establishment, object>>[]
            {
                e => e.SamlSignOn,
            };

            RuleFor(p => p.EmailAddress)
                //cannot be empty
                .NotEmpty()
                    .WithMessage(ValidateEmailAddress.FailedBecauseValueWasEmpty)
                // must be valid against email address regular expression
                .EmailAddress()
                    .WithMessage(ValidateEmailAddress.FailedBecauseValueWasNotValidEmailAddress)
                // must match a person
                .Must(p => ValidateEmailAddress.ValueMatchesPerson(p, queryProcessor, loadPerson, out person))
                    .WithMessage(ValidateEmailAddress.FailedBecauseValueMatchedNoPerson,
                        p => p.EmailAddress)
                // must match an establishment
                .Must(p => ValidateEstablishment.EmailMatchesEntity(p, queryProcessor, loadEstablishment, out establishment))
                    .WithMessage(ValidateEstablishment.FailedBecauseEmailMatchedNoEntity, 
                        p => p.EmailAddress)
                // establishment must be a member
                .Must(p => establishment.IsMember)
                    .WithMessage(ValidateEstablishment.FailedBecauseEstablishmentIsNotMember,
                        p => establishment.RevisionId)
            ;

            // when person is not null and intent is to reset password,
            When(p => person != null && p.Intent == EmailConfirmationIntent.ResetPassword, () =>
                RuleFor(p => p.EmailAddress)
                    // the establishment must not have saml sign on
                    .Must(p => !establishment.HasSamlSignOn())
                        .WithMessage(ValidateEstablishment.FailedBecauseEstablishmentHasSamlSignOn,
                            p => establishment.RevisionId)
                    // the matched person must have a user
                    .Must(p => ValidatePerson.UserIsNotNull(person))
                        .WithMessage(ValidatePerson.FailedBecauseUserWasNull,
                            p => person.DisplayName)
                    // the user must not have a SAML account
                    .Must(p => ValidateUser.EduPersonTargetedIdIsEmpty(person.User))
                        .WithMessage(ValidateUser.FailedBecauseEduPersonTargetedIdWasNotEmpty,
                            p => person.User.Name)
                    // the email address' person's user's name must match a local member account
                    .Must(p => ValidateUser.NameMatchesLocalMember(person.User.Name, passwords))
                        .WithMessage(ValidateUser.FailedBecauseNameMatchedNoLocalMember,
                            p => person.User.Name)
                    // the email address must be confirmed
                    .Must(p => ValidateEmailAddress.IsConfirmed(person.GetEmail(p)))
                        .WithMessage(ValidateEmailAddress.FailedBecauseIsNotConfirmed,
                            p => p.EmailAddress)
            );
        }
    }
}
