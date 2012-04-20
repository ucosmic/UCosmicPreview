using System.Security.Principal;

namespace UCosmic.Domain.Identity
{
    internal static class ValidatePrincipal
    {
        internal const string FailedWithEmptyIdentityName =
            "The principal identity name is required.";

        internal static bool IdentityNameIsNotEmpty(IPrincipal principal)
        {
            return !string.IsNullOrWhiteSpace(principal.Identity.Name);
        }

        internal const string FailedWithNoUserMatchesIdentityName =
            "The principal identity name '{0}' does not have a user account.";

        internal static bool IdentityNameMatchesUser(IPrincipal principal, IProcessQueries queryProcessor)
        {
            var user = queryProcessor.Execute(
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
