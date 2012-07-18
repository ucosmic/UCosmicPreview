using System;
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
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public PurgeMyActivityHandler(IProcessQueries queryProcessor, ICommandEntities entities)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(PurgeMyActivityCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var activity = _queryProcessor.Execute(
                new GetMyActivityByNumberQuery
                {
                    Principal = command.Principal,
                    Number = command.Number,
                }
            );

            _entities.Purge(activity);
        }
    }
}
