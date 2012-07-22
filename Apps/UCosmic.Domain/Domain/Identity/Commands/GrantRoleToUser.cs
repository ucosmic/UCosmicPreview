using System;
using System.Linq.Expressions;

namespace UCosmic.Domain.Identity
{
    public class GrantRoleToUserCommand
    {
        public GrantRoleToUserCommand(Guid roleGuid, Guid userGuid)
        {
            if (roleGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "roleGuid");
            if (userGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "userGuid");
            RoleGuid = roleGuid;
            UserGuid = userGuid;
        }

        internal GrantRoleToUserCommand(Role role, Guid userGuid)
        {
            if (role == null) throw new ArgumentNullException("role");
            if (userGuid == Guid.Empty) throw new ArgumentException("Cannot be empty", "userGuid");
            Role = role;
            UserGuid = userGuid;
        }

        public Guid RoleGuid { get; private set; }
        public Guid UserGuid { get; private set; }
        public bool IsNewlyGranted { get; internal set; }
        internal Role Role { get; set; }
    }

    public class GrantRoleToUserHandler : IHandleCommands<GrantRoleToUserCommand>
    {
        private readonly ICommandEntities _entities;

        public GrantRoleToUserHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(GrantRoleToUserCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var role = command.Role ??
                _entities.Get2<Role>()
                .EagerLoad(new Expression<Func<Role, object>>[]
                {
                    r => r.Grants,
                }, _entities)
                .By(command.RoleGuid);

            var grant = role.Grants.ByUser(command.UserGuid);
            if (grant != null) return;

            var user = _entities.Get2<User>().By(command.UserGuid);
            grant = new RoleGrant
            {
                Role = role,
                User = user,
            };
            _entities.Create(grant);
            command.IsNewlyGranted = true;
        }
    }
}
