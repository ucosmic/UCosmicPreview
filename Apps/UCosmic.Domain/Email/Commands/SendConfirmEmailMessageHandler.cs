using System;
using System.Linq.Expressions;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
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
            var confirmation = new EmailConfirmation
                (email, command.Intent);
            command.ConfirmationToken = confirmation.Token;
            _entities.Create(confirmation);

            // get the email template
            var template = _queryProcessor.Execute(
                new GetEmailTemplateByNameQuery
                {
                    Name = command.EmailTemplateName,
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
}
