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

        public static bool IdMatchesEntity(int id, IQueryEntities entities,
            IEnumerable<Expression<Func<Establishment, object>>> eagerLoad, out Establishment entity)
        {
            if (id < 0)
            {
                entity = null;
                return false;
            }

            entity = entities.Read<Establishment>()
                .EagerLoad(eagerLoad, entities).By(id);

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool IdMatchesEntity(int id, IQueryEntities entities,
            IEnumerable<Expression<Func<Establishment, object>>> eagerLoad = null)
        {
            Establishment entity;
            return IdMatchesEntity(id, entities, eagerLoad, out entity);
        }

        public static bool IdMatchesEntity(int id, IQueryEntities entities, out Establishment entity)
        {
            return IdMatchesEntity(id, entities, null, out entity);
        }

        #endregion
        #region Email matches entity

        public const string FailedBecauseEmailMatchedNoEntity =
            "Establishment for email '{0}' could not be found.";

        public static bool EmailMatchesEntity(string email, IQueryEntities entities,
            IEnumerable<Expression<Func<Establishment, object>>> eagerLoad, out Establishment entity)
        {
            entity = entities.Read<Establishment>().EagerLoad(eagerLoad, entities).ByEmail(email);

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool EmailMatchesEntity(string email, IQueryEntities entities,
            IEnumerable<Expression<Func<Establishment, object>>> eagerLoad = null)
        {
            Establishment entity;
            return EmailMatchesEntity(email, entities, eagerLoad, out entity);
        }

        public static bool EmailMatchesEntity(string email, IQueryEntities entities, out Establishment entity)
        {
            return EmailMatchesEntity(email, entities, null, out entity);
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
