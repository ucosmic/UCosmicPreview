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
