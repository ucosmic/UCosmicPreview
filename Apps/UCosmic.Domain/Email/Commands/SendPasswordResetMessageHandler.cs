using System;
using System.Linq.Expressions;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class SendPasswordResetMessageHandler : IHandleCommands<SendPasswordResetMessageCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly IHandleCommands<SendEmailMessageCommand> _sendHandler;

        public SendPasswordResetMessageHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
            , IHandleCommands<SendEmailMessageCommand> sendHandler
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _sendHandler = sendHandler;
        }

        public void Handle(SendPasswordResetMessageCommand command)
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
            if (person == null) throw new InvalidOperationException(string.Format(
                ValidateEmailAddress.FailedBecauseValueMatchedNoPerson,
                    command.EmailAddress));

            // get the email
            var email = person.Emails.ByValue(command.EmailAddress);
            if (email == null) throw new InvalidOperationException(string.Format(
                ValidateEmailAddress.FailedBecauseValueMatchedNoPerson,
                    command.EmailAddress));

            // create the confirmation
            var confirmation = new EmailConfirmation
                (EmailConfirmationIntent.PasswordReset, 12, email);
            command.ConfirmationToken = confirmation.Token;
            _entities.Create(confirmation);

            // get the email template
            var template = _queryProcessor.Execute(
                new GetEmailTemplateByNameQuery
                {
                    Name = EmailTemplateName.PasswordResetConfirmation,
                }
            );

            // create the message
            var message = _queryProcessor.Execute(
                new ComposeEmailMessageQuery(template, email)
                {
                    Formatters = _queryProcessor.Execute(
                        new GetEmailConfirmationFormattersQuery(confirmation)
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
}
