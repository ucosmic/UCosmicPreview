using FluentValidation;
using UCosmic.Domain.Identity;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class UpdateNameValidator : AbstractValidator<UpdateNameCommand>
    {
        private readonly IProcessQueries _queryProcessor;

        public UpdateNameValidator(IProcessQueries queryProcessor)
        {
            _queryProcessor = queryProcessor;

            RuleFor(p => p.DisplayName)
                .NotEmpty()
            ;

            RuleFor(p => p.Principal).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(HaveNonEmptyIdentityName).WithMessage(PrincipalIdentityNameIsEmptyMessage)
                .Must(MatchExistingUser).WithMessage(PrincipalIdentityNameDoesNotMatchExistingUserErrorFormat, p => p.Principal.Identity.Name)
            ;
        }

        internal const string PrincipalIdentityNameIsEmptyMessage =
            "The principal identity name is required.";

        internal const string PrincipalIdentityNameDoesNotMatchExistingUserErrorFormat =
            "The principal identity name '{0}' does not have a user account.";

        private static bool HaveNonEmptyIdentityName(IPrincipal principal)
        {
            return !string.IsNullOrWhiteSpace(principal.Identity.Name);
        }

        private bool MatchExistingUser(UpdateNameCommand command, IPrincipal principal)
        {
            var user = _queryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = principal.Identity.Name,
                }
            );

            // return true (valid) if there is a user
            return user != null;
        }
    }
}
