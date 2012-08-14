using System;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.Identity
{
    public class UpdateMyPreference
    {
        public UpdateMyPreference(IPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Principal = principal;
        }

        public IPrincipal Principal { get; private set; }
        public Enum Category { get; set; }
        public Enum Key { get; set; }
        public string Value { get; set; }
    }

    public class HandleUpdateMyPreferenceCommand : IHandleCommands<UpdateMyPreference>
    {
        private readonly ICommandEntities _entities;

        public HandleUpdateMyPreferenceCommand(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(UpdateMyPreference command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var preference = _entities.Get<Preference>()
                .ByPrincipal(command.Principal)
                .ByCategory(command.Category)
                .ByKey(command.Key)
                .SingleOrDefault();

            if (preference == null)
            {
                var user = _entities.Get<User>().ByPrincipal(command.Principal);
                preference = new Preference
                {
                    User = user,
                    UserId = user.RevisionId,
                    Category = command.Category.ToString(),
                    Key = command.Key.ToString(),
                    Value = command.Value,
                };
                _entities.Create(preference);
            }
            else
            {
                preference.Value = command.Value;
                _entities.Update(preference);
            }
        }
    }
}
