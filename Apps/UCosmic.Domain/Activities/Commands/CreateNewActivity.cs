using System;
using System.Security.Principal;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Activities
{
    public class CreateNewActivityCommand
    {
        public IPrincipal Principal { get; set; }
        public Activity CreatedActivity { get; internal set; }
    }

    public class CreateNewActivityHandler : IHandleCommands<CreateNewActivityCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public CreateNewActivityHandler(IProcessQueries queryProcessor, ICommandEntities entities)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(CreateNewActivityCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var person = _queryProcessor.Execute(
                new GetMyPersonQuery
                {
                    Principal = command.Principal,
                }
            );

            var otherActivities = _queryProcessor.Execute(
                new FindActivitiesWithPersonIdQuery
                {
                    PersonId = person.RevisionId,
                }
            );

            var activity = new Activity
            {
                Person = person,
                PersonId = person.RevisionId,
                Number = otherActivities.NextNumber(),
            };
            _entities.Create(activity);
            command.CreatedActivity = activity;
        }
    }
}
