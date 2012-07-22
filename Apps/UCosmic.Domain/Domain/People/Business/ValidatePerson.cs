using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    public static class ValidatePerson
    {
        #region DisplayName cannot be empty

        public const string FailedBecauseDisplayNameWasEmpty =
            "A person's display name cannot be empty.";

        #endregion
        #region PersonId matches entity

        public const string FailedBecauseIdMatchedNoEntity =
            "Person with id '{0}' could not be found.";

        public static bool IdMatchesEntity(int id, IQueryEntities entities,
            IEnumerable<Expression<Func<Person, object>>> eagerLoad, out Person entity)
        {
            if (id < 0)
            {
                entity = null;
                return false;
            }

            entity = entities.Read<Person>()
                .EagerLoad(eagerLoad, entities).By(id);

            // return true (valid) if there is an entity
            return entity != null;
        }

        public static bool IdMatchesEntity(int id, IQueryEntities entities,
            IEnumerable<Expression<Func<Person, object>>> eagerLoad = null)
        {
            Person entity;
            return IdMatchesEntity(id, entities, eagerLoad, out entity);
        }

        public static bool IdMatchesEntity(int id, IQueryEntities entities, out Person entity)
        {
            return IdMatchesEntity(id, entities, null, out entity);
        }

        #endregion
        #region Person is not already affiliated with establishment

        public const string FailedBecausePersonIsAlreadyAffiliatedWithEstablishment =
            "Person '{0}' is already affiliated with establishment '{1}'.";

        public static bool IsNotAlreadyAffiliatedWithEstablishment(Person person, int establishmentId)
        {
            // return true (valid) if person does not have matching affiliation
            return person != null && person.GetAffiliation(establishmentId) == null;
        }

        #endregion
        #region User cannot be null

        public const string FailedBecauseUserWasNull =
            "The person '{0}' is not associated with a user.";

        public static bool UserIsNotNull(Person entity)
        {
            return entity != null && entity.User != null;
        }

        #endregion
        #region User cannot be registered

        public const string FailedBecauseUserIsRegistered =
            "The person '{0}' is already associated with a registered user.";

        #endregion
    }
}
