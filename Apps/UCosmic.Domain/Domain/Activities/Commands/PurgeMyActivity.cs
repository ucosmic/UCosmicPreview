using System;
using System.Linq.Expressions;
using System.Security.Principal;

namespace UCosmic.Domain.Activities
{
    public class PurgeMyActivityCommand
    {
        public IPrincipal Principal { get; set; }
        public int Number { get; set; }
    }

    public class PurgeMyActivityHandler : IHandleCommands<PurgeMyActivityCommand>
    {
        private readonly ICommandEntities _entities;

        public PurgeMyActivityHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(PurgeMyActivityCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var activity = _entities.Get2<Activity>()
                .EagerLoad(new Expression<Func<Activity, object>>[]
                {
                    t => t.Tags,
                    t => t.DraftedTags,
                }, _entities)
                .ByUserNameAndNumber(command.Principal.Identity.Name, command.Number);

            _entities.Purge(activity);
        }
    }
}
