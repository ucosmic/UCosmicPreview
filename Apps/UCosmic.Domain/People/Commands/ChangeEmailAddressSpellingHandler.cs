namespace UCosmic.Domain.People
{
    public class ChangeEmailAddressSpellingHandler : IHandleCommands<ChangeEmailAddressSpellingCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public ChangeEmailAddressSpellingHandler(IProcessQueries queryProcessor, ICommandEntities entities)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(ChangeEmailAddressSpellingCommand command)
        {
            command.ChangedState = false;

            // get the email address
            var email = _queryProcessor.Execute(
                new GetEmailAddressByUserNameAndNumberQuery
                {
                    UserName = command.UserName,
                    Number = command.Number,
                }
            );

            // only process matching email
            if (email == null) return;

            // only update the value if it was respelled
            if (email.Value == command.NewValue) return;

            email.Value = command.NewValue;
            _entities.Update(email);
            command.ChangedState = true;
        }
    }
}
