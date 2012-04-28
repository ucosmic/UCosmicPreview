using System;
using System.Linq.Expressions;
using FluentValidation;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class SendConfirmEmailMessageCommand
    {
        public string EmailAddress { get; set; }
        public string Intent { get; set; }

        internal string TemplateName
        {
            get
            {
                switch (Intent)
                {
                    case EmailConfirmationIntent.PasswordReset:
                        return EmailTemplateName.PasswordResetConfirmation;

                    case EmailConfirmationIntent.SignUp:
                        return EmailTemplateName.SignUpConfirmation;
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
            var confirmation = new EmailConfirmation
            {
                EmailAddress = email,
                Intent = command.Intent,
                SecretCode = secretCode,
            };
            command.ConfirmationToken = confirmation.Token;
            _entities.Create(confirmation);

            // get the email template
            var template = _queryProcessor.Execute(
                new GetEmailTemplateByNameQuery
                {
                    Name = command.TemplateName,
                }
            );

            // create the message
            var message = _queryProcessor.Execute(
                new ComposeEmailMessageQuery(template, email)
                {
                    Formatters = _queryProcessor.Execute(
                        new GetConfirmEmailFormattersQuery(confirmation)
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
        public SendConfirmEmailMessageValidator(IProcessQueries queryProcessor, ISignMembers memberSigner)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            Person person = null;
            var eagerLoad = new Expression<Func<Person, object>>[]
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
                // the email address must match a person
                .Must(p => ValidateEmailAddress.ValueMatchesPerson(p, queryProcessor, eagerLoad, out person))
                    .WithMessage(ValidateEmailAddress.FailedBecauseValueMatchedNoPerson,
                        p => p.EmailAddress)
            ;

            // when person is not null,
            When(p => person != null, () =>
                RuleFor(p => p.EmailAddress)
                    // the matched person must have a user
                    .Must(p => ValidatePerson.UserIsNotNull(person))
                        .WithMessage(ValidatePerson.FailedBecauseUserWasNull,
                            p => person.DisplayName)
                    // the user must not have a SAML account
                    .Must(p => ValidateUser.EduPersonTargetedIdIsEmpty(person.User))
                        .WithMessage(ValidateUser.FailedBecauseEduPersonTargetedIdWasNotEmpty,
                            p => person.User.Name)
                    // the email address' person's user's name must match a local member account
                    .Must(p => ValidateUser.NameMatchesLocalMember(person.User.Name, memberSigner))
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
