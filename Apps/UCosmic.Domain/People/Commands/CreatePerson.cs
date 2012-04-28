using System;
using FluentValidation;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class CreatePersonCommand
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool UserIsRegistered { get; set; }
        public Person CreatedPerson { get; internal set; }
    }

    public class CreatePersonHandler : IHandleCommands<CreatePersonCommand>
    {
        private readonly ICommandEntities _entities;

        public CreatePersonHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(CreatePersonCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            // construct the person
            var person = new Person
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                DisplayName = command.DisplayName,
            };

            // attach a user if commanded
            if (!string.IsNullOrWhiteSpace(command.UserName))
                person.User = new User
                {
                    Name = command.UserName,
                    IsRegistered = command.UserIsRegistered,
                };

            // store
            _entities.Create(person);

            command.CreatedPerson = person;
        }
    }

    public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public CreatePersonValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.DisplayName)

                // display name cannot be empty
                .NotEmpty().WithMessage(
                    ValidatePerson.FailedBecauseDisplayNameWasEmpty)
            ;

            RuleFor(p => p.UserName)

                // if username is present, validate that it is not attached to another person
                .Must(ValidateUserNameMatchesNoEntity)
                    .WithMessage(ValidateUser.FailedBecauseNameMatchedEntity,
                        p => p.UserName)
            ;
        }

        private bool ValidateUserNameMatchesNoEntity(CreatePersonCommand command, string userName)
        {
            return ValidateUser.NameMatchesNoEntity(userName, _queryProcessor);
        }
    }
}
