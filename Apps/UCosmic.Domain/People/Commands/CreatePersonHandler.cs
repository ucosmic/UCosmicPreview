using System;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class CreatePersonHandler : IHandleCommands<CreatePersonCommand>
    {
        private readonly ICommandEntities _entities;

        public CreatePersonHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(CreatePersonCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // construct the person
            var person = new Person
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                DisplayName = command.DisplayName,
            };

            // attach a user if commanded
            if (!string.IsNullOrWhiteSpace(command.UserName))
                person.User = new User
                {
                    Name = command.UserName,
                    IsRegistered = command.UserIsRegistered,
                };

            // store
            _entities.Create(person);

            command.CreatedPerson = person;
        }
    }
}
