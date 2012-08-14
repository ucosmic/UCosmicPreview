using System;
using System.Linq;
using System.Security.Principal;

namespace UCosmic.Domain.Identity
{
    public class UpdateMyPreference
    {
        public UpdateMyPreference(IPrincipal principal)
        {
            Principal = principal;
        }

        public UpdateMyPreference(IPrincipal principal, string anonymousId)
            : this(principal)
        {
            AnonymousId = anonymousId;
        }

        public IPrincipal Principal { get; private set; }
        public string AnonymousId { get; private set; }
        public bool HasPrincipal { get { return Principal != null && !string.IsNullOrWhiteSpace(Principal.Identity.Name); } }
        public bool IsAnonymous { get { return !string.IsNullOrWhiteSpace(AnonymousId); } }
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

            var preferences = _entities.Get<Preference>();

            if (command.HasPrincipal)
                preferences = preferences.ByPrincipal(command.Principal);
            else if (command.IsAnonymous)
                preferences = preferences.ByAnonymousId(command.AnonymousId);

            var preference = preferences
                .ByCategory(command.Category)
                .ByKey(command.Key)
                .SingleOrDefault();

            if (preference == null)
            {
                if (command.HasPrincipal)
                    preference = new Preference(_entities.Get<User>().ByPrincipal(command.Principal));

                else if (command.IsAnonymous)
                    preference = new Preference(command.AnonymousId);

                else return;

                preference.Category = command.Category.ToString();
                preference.Key = command.Key.ToString();
                preference.Value = command.Value;
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
