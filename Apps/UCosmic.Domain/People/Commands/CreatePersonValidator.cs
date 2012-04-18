using System;
using System.Linq.Expressions;
using FluentValidation;
using UCosmic.Domain.Identity;

namespace UCosmic.Domain.People
{
    public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public CreatePersonValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            RuleFor(p => p.DisplayName)
                .NotEmpty()
            ;

            // if user is present, validate that it is not attached to another person
            RuleFor(p => p.UserName)
                .Must(NotBeAssociatedWithAnotherPerson)
                    .WithMessage(UserIsAlreadyAssociatedWithAnotherPersonErrorFormat,
                        p => p.UserName, p => p.DisplayName, p => p.UserPersonDisplayName)
            ;
        }

        public const string UserIsAlreadyAssociatedWithAnotherPersonErrorFormat =
            "The user '{0}' cannot be associated with person '{1}' because it is already associated with person '{2}'.";

        private bool NotBeAssociatedWithAnotherPerson(CreatePersonCommand command, string userName)
        {
            // when username is not provided, do not validate
            if (string.IsNullOrWhiteSpace(userName)) return true;

            var user = _queryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = userName,
                    EagerLoad = new Expression<Func<User, object>>[]
                    {
                        u => u.Person,
                    },
                }
            );

            command.UserPersonDisplayName = user != null && user.Person != null
                ? user.Person.DisplayName : null;

            // return true (valid) if there is no user or no person attached to the user
            return user == null || user.Person == null;
        }
    }
}
