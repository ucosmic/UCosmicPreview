using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Establishments
{
    public static class ValidateEstablishment
    {
        #region EstablishmentId matches entity

        public const string FailedBecauseIdMatchedNoEntity =
            "Establishment with id '{0}' could not be found.";

        public static bool IdMatchesEntity(int id, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<Establishment, object>>> eagerLoad, out Establishment entity)
        {
            if (id < 0)
            {
                entity = null;
                return false;
            }

            entity = queryProcessor.Execute(
                new GetEstablishmentByIdQuery
                {
                    Id = id,
                    EagerLoad = eagerLoad,
                }
            );

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool IdMatchesEntity(int id, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<Establishment, object>>> eagerLoad = null)
        {
            Establishment entity;
            return IdMatchesEntity(id, queryProcessor, eagerLoad, out entity);
        }

        public static bool IdMatchesEntity(int id, IProcessQueries queryProcessor, out Establishment entity)
        {
            return IdMatchesEntity(id, queryProcessor, null, out entity);
        }

        #endregion
        #region Email matches entity

        public const string FailedBecauseEmailMatchedNoEntity =
            "Establishment for email '{0}' could not be found.";

        public static bool EmailMatchesEntity(string email, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<Establishment, object>>> eagerLoad, out Establishment entity)
        {
            entity = queryProcessor.Execute(
                new GetEstablishmentByEmailQuery
                {
                    Email = email,
                    EagerLoad = eagerLoad,
                }
            );

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool EmailMatchesEntity(string email, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<Establishment, object>>> eagerLoad = null)
        {
            Establishment entity;
            return EmailMatchesEntity(email, queryProcessor, eagerLoad, out entity);
        }

        public static bool EmailMatchesEntity(string email, IProcessQueries queryProcessor, out Establishment entity)
        {
            return EmailMatchesEntity(email, queryProcessor, null, out entity);
        }

        #endregion
        #region Establishment is member

        public const string FailedBecauseEstablishmentIsNotMember =
            "Establishment with id '{0}' is not a member.";

        #endregion
        #region Establishment does not have a saml sign on

        public const string FailedBecauseEstablishmentHasSamlSignOn =
            "Establishment with id '{0}' is SAML-enabled.";

        #endregion
    }
}
