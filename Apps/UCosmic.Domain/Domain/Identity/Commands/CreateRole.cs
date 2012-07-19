using System;

namespace UCosmic.Domain.Identity
{
    public class CreateRoleCommand
    {
        public CreateRoleCommand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cannot be null or white space.", "name");
            Name = name;
        }

        public string Name { get; private set; }
        public string Description { get; set; }
    }

    public class CreateRoleHandler : IHandleCommands<CreateRoleCommand>
    {
        private readonly ICommandEntities _entities;

        public CreateRoleHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(CreateRoleCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var entity = new Role
            {
                Name = command.Name,
                Description = command.Description,
            };

            _entities.Create(entity);
        }
    }
}
