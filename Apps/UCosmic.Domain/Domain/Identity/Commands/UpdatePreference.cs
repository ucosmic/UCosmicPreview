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
        public PreferenceCategory Category { get; set; }
        public Enum Key { get; set; }
        public string Value { get; set; }
        internal string KeyText
        {
            get { return Key.ToString(); }
        }
    }

    public class HandleUpdateMyPreferenceCommand : IHandleCommands<UpdateMyPreference>
    {
        private readonly IProcessQueries _queries;
        private readonly ICommandEntities _entities;

        public HandleUpdateMyPreferenceCommand(IProcessQueries queries, ICommandEntities entities)
        {
            _queries = queries;
            _entities = entities;
        }

        public void Handle(UpdateMyPreference command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var preferences = _queries.Execute(new MyPreferencesByCategory(command.Principal)
            {
                Category = command.Category,
            });

            var preference = preferences.ByKey(command.Key).SingleOrDefault();
            if (preference == null)
            {
                var user = _queries.Execute(new GetUserByNameQuery { Name = command.Principal.Identity.Name });
                preference = new Preference
                {
                    User = user,
                    UserId = user.RevisionId,
                    Category = command.Category,
                    Key = command.KeyText,
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
