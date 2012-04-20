using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.Establishments
{
    internal static class ValidateEstablishment
    {
        #region EstablishmentId matches entity

        internal const string FailedBecauseIdMatchedNoEntity =
            "Establishment with id '{0}' could not be found.";

        internal static bool IdMatchesEntity(int id, IProcessQueries queryProcessor,
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

        internal static bool IdMatchesEntity(int id, IProcessQueries queryProcessor,
            IEnumerable<Expression<Func<Establishment, object>>> eagerLoad = null)
        {
            Establishment entity;
            return IdMatchesEntity(id, queryProcessor, eagerLoad, out entity);
        }

        internal static bool IdMatchesEntity(int id, IProcessQueries queryProcessor, out Establishment entity)
        {
            return IdMatchesEntity(id, queryProcessor, null, out entity);
        }

        #endregion
    }
}
