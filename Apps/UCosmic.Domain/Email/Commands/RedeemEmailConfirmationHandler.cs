using System;

namespace UCosmic.Domain.Email
{
    public class RedeemEmailConfirmationHandler : IHandleCommands<RedeemEmailConfirmationCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public RedeemEmailConfirmationHandler(IProcessQueries queryProcessor
            , ICommandEntities entities
        )
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(RedeemEmailConfirmationCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the confirmation
            var confirmation = _queryProcessor.Execute(
                new GetEmailConfirmationQuery(command.Token)
            );

            confirmation.RedeemedOnUtc = DateTime.UtcNow;
            confirmation.SecretCode = null;
            confirmation.Ticket = RandomSecretCreator.CreateSecret(256);
            command.Ticket = confirmation.Ticket;
            _entities.Update(confirmation);
        }
    }
}
