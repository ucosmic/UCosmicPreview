using System;
using System.Linq;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    public class CreateAffiliationHandler : IHandleCommands<CreateAffiliationCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public CreateAffiliationHandler(IProcessQueries queryProcessor, ICommandEntities entities)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(CreateAffiliationCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the person
            var person = _queryProcessor.Execute(
                new GetPersonByIdQuery
                {
                    Id = command.PersonId,
                    EagerLoad = new Expression<Func<Person, object>>[]
                    {
                        p => p.Affiliations,
                    },
                }
            );

            // construct the affiliation
            var affiliation = new Affiliation
            {
                EstablishmentId = command.EstablishmentId,
                IsClaimingStudent = command.IsClaimingStudent,
                IsClaimingEmployee = command.IsClaimingEmployee,
                IsDefault = !person.Affiliations.Any(a => a.IsDefault),
            };
            person.Affiliations.Add(affiliation);

            // store
            _entities.Create(affiliation);
        }
    }
}
