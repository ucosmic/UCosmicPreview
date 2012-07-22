using System;
using System.Linq.Expressions;
using System.Threading;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Identity
{
    public class SendEmailMessageCommand
    {
        public int PersonId { get; set; }
        public int MessageNumber { get; set; }
    }

    public class SendEmailMessageHandler : IHandleCommands<SendEmailMessageCommand>
    {
        private int _retryCount;
        private const int RetryLimit = 1000;
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly ISendMail _mailSender;
        private readonly ILogExceptions _exceptionLogger;

        public SendEmailMessageHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
            , ISendMail mailSender
            , ILogExceptions exceptionLogger
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _mailSender = mailSender;
            _exceptionLogger = exceptionLogger;
        }

        public void Handle(SendEmailMessageCommand command)
        {
            // get a fresh email address from the database
            EmailMessage emailMessage = null;
            while (emailMessage == null && ++_retryCount < RetryLimit)
            {
                if (_retryCount > 1) Thread.Sleep(300);

                var person = _entities.Get<Person>()
                    .EagerLoad(new Expression<Func<Person, object>>[]
                    {
                        p => p.Messages,
                    }, _entities)
                    .By(command.PersonId);
                emailMessage = person != null ? person.GetMessage(command.MessageNumber) : null;
            }
            if (emailMessage == null)
            {
                var exception = new OperationCanceledException(string.Format(
                    "Unable to locate EmailMessage number '{0}' for person '{1}'. The message send operation was canceled after {2} retries.",
                    command.MessageNumber, command.PersonId, _retryCount));
                _exceptionLogger.LogException(exception);
                throw exception;
            }

            // convert email message to mail message
            var mail = _queryProcessor.Execute(
                new ComposeMailMessageQuery(emailMessage)
            );

            // send the mail message
            _mailSender.Send(mail);

            // log when the message was sent
            emailMessage.SentOnUtc = DateTime.UtcNow;
            _entities.Update(emailMessage);
        }
    }

}
