using System;
using System.Linq.Expressions;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class UpdateNameHandler : IHandleCommands<UpdateNameCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public UpdateNameHandler(IProcessQueries queryProcessor, ICommandEntities entities)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(UpdateNameCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // get the person for the principal
            var user = _queryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = command.Principal.Identity.Name,
                    EagerLoad = new Expression<Func<User, object>>[]
                    {
                        u => u.Person,
                    },
                }
            );

            // update fields
            if (user.Person.IsDisplayNameDerived != command.IsDisplayNameDerived) command.ChangeCount++;
            user.Person.IsDisplayNameDerived = command.IsDisplayNameDerived;

            if (user.Person.DisplayName != command.DisplayName) command.ChangeCount++;
            user.Person.DisplayName = command.IsDisplayNameDerived
                ? _queryProcessor.Execute(
                    new GenerateDisplayNameQuery
                    {
                        Salutation = command.Salutation,
                        FirstName = command.FirstName,
                        MiddleName = command.MiddleName,
                        LastName = command.LastName,
                        Suffix = command.Suffix,
                    })
                : command.DisplayName;


            if (user.Person.Salutation != command.Salutation) command.ChangeCount++;
            user.Person.Salutation = command.Salutation;

            if (user.Person.FirstName != command.FirstName) command.ChangeCount++;
            user.Person.FirstName = command.FirstName;

            if (user.Person.MiddleName != command.MiddleName) command.ChangeCount++;
            user.Person.MiddleName = command.MiddleName;

            if (user.Person.LastName != command.LastName) command.ChangeCount++;
            user.Person.LastName = command.LastName;

            if (user.Person.Suffix != command.Suffix) command.ChangeCount++;
            user.Person.Suffix = command.Suffix;

            // store
            if (command.ChangeCount > 0) _entities.Update(user.Person);
        }
    }
}
