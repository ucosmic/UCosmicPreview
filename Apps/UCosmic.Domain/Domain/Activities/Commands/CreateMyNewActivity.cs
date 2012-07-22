using System;
using System.Security.Principal;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Activities
{
    public class CreateMyNewActivityCommand
    {
        public IPrincipal Principal { get; set; }
        public Activity CreatedActivity { get; internal set; }
    }

    public class CreateMyNewActivityHandler : IHandleCommands<CreateMyNewActivityCommand>
    {
        private readonly ICommandEntities _entities;

        public CreateMyNewActivityHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(CreateMyNewActivityCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            //var person = _queryProcessor.Execute(
            //    new GetMyPersonQuery(command.Principal));
            var person = _entities.Get2<Person>()
                .ByUserName(command.Principal.Identity.Name);

            var otherActivities = _entities.Read<Activity>()
                .WithPersonId(person.RevisionId)
            ;

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
