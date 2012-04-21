using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Identity
{
    public static class ValidateUser
    {
        #region Name does not match entity

        public const string FailedBecauseNameMatchedEntity =
            "User with name '{0}' cannot be created because it already exists.";

        public static bool NameMatchesNoEntity(string name, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<User, object>>> eagerLoad, out User entity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                entity = null;
                return true;
            }

            entity = queryProcessor.Execute(
                new GetUserByNameQuery
                {
                    Name = name,
                    EagerLoad = eagerLoad,
                }
            );

            // return true (valid) if there is no entity
            return entity == null;
        }

        public static bool NameMatchesNoEntity(string name, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<User, object>>> eagerLoad = null)
        {
            User entity;
            return NameMatchesNoEntity(name, queryProcessor, eagerLoad, out entity);
        }

        public static bool NameMatchesNoEntity(string name, IProcessQueries queryProcessor, out User entity)
        {
            return NameMatchesNoEntity(name, queryProcessor, null, out entity);
        }

        #endregion
    }
}
