using System;
using System.Linq.Expressions;
using System.Security.Principal;
using FluentValidation;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class UpdateMyNameCommand
    {
        public IPrincipal Principal { get; set; }
        public bool IsDisplayNameDerived { get; set; }
        public string DisplayName { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int ChangeCount { get; internal set; }
        public bool ChangedState { get { return ChangeCount > 0; } }
    }

    public class UpdateMyNameHandler : IHandleCommands<UpdateMyNameCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;

        public UpdateMyNameHandler(IProcessQueries queryProcessor, ICommandEntities entities)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
        }

        public void Handle(UpdateMyNameCommand command)
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

    public class UpdateMyNameValidator : AbstractValidator<UpdateMyNameCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public UpdateMyNameValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.DisplayName)

                // display name cannot be empty
                .NotEmpty().WithMessage(
                    ValidatePerson.FailedBecauseDisplayNameWasEmpty)
            ;

            RuleFor(p => p.Principal)

                // principal cannot be null
                .NotEmpty().WithMessage(
                    ValidatePrincipal.FailedBecausePrincipalWasNull)

                // principal identity name cannot be null or whitespace
                .Must(ValidatePrincipal.IdentityNameIsNotEmpty).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameWasEmpty)

                // principal identity name must match User.Name entity property
                .Must(ValidatePrincipalIdentityNameMatchesUser).WithMessage(
                    ValidatePrincipal.FailedBecauseIdentityNameMatchedNoUser,
                        p => p.Principal.Identity.Name)
            ;
        }

        private bool ValidatePrincipalIdentityNameMatchesUser(IPrincipal principal)
        {
            return ValidatePrincipal.IdentityNameMatchesUser(principal, _queryProcessor);
        }
    }
}
