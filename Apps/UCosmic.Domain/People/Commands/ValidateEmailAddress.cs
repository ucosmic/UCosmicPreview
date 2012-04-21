using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public static class ValidateEmailAddress
    {
        #region Value cannot be empty

        public const string FailedBecauseValueWasEmpty =
            "Email value is required.";

        #endregion
        #region Value must be valid email address

        public const string FailedBecauseValueWasNotValidEmailAddress =
            "Email '{0}' is not a valid email address.";

        #endregion
        #region Number and Principal matches entity

        public const string FailedBecauseNumberAndPrincipalMatchedNoEntity =
            "Email with number '{0}' could not be found for user.";

        public static bool NumberAndPrincipalMatchesEntity(int number, IPrincipal principal, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<Person, object>>> eagerLoad, out EmailAddress entity)
        {
            if (queryProcessor == null)
            {
                entity = null;
                return false;
            }

            entity = queryProcessor.Execute(
                new GetMyEmailAddressByNumberQuery
                {
                    Principal = principal,
                    Number = number,
                }
            );

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool NumberAndPrincipalMatchesEntity(int number, IPrincipal principal, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<Person, object>>> eagerLoad = null)
        {
            EmailAddress entity;
            return NumberAndPrincipalMatchesEntity(number, principal, queryProcessor, eagerLoad, out entity);
        }

        public static bool NumberAndPrincipalMatchesEntity(int number, IPrincipal principal, IProcessQueries queryProcessor, out EmailAddress entity)
        {
            return NumberAndPrincipalMatchesEntity(number, principal, queryProcessor, null, out entity);
        }

        #endregion
        #region New value matches previous value case invariantly

        public const string FailedBecauseNewValueDidNotMatchCurrentValueCaseInvsensitively =
            "Email address '{0}' does not match previous spelling case insensitively.";

        public static bool NewValueMatchesCurrentValueCaseInsensitively(string newValue, EmailAddress email)
        {
            return email != null && email.Value.Equals(newValue, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
