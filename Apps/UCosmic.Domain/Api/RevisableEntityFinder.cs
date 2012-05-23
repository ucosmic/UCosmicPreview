using System.Collections.Generic;
using System.Linq;
using System;

namespace UCosmic.Domain
{
    public abstract class RevisableEntityFinder<TEntity> : EntityFinder<TEntity> where TEntity : RevisableEntity
    {
        protected RevisableEntityFinder(IQueryEntities entityQueries) : base(entityQueries)
        {
        }

        public sealed override ICollection<TEntity> FindMany(EntityQueryCriteria<TEntity> criteria)
        {
            return FindMany((RevisableEntityQueryCriteria<TEntity>)criteria);
        }

        public sealed override TEntity FindOne(EntityQueryCriteria<TEntity> criteria)
        {
            return FindOne((RevisableEntityQueryCriteria<TEntity>)criteria);
        }

        public abstract ICollection<TEntity> FindMany(RevisableEntityQueryCriteria<TEntity> criteria);
        public virtual TEntity FindOne(RevisableEntityQueryCriteria<TEntity> criteria)
        {
            return FindMany(criteria).SingleOrDefault();
        }

        protected IQueryable<TEntity> InitializeQuery(IQueryable<TEntity> query, RevisableEntityQueryCriteria<TEntity> criteria)
        {
            query = base.InitializeQuery(query, criteria);

            // apply primary key
            if (criteria.RevisionId.HasValue && criteria.RevisionId != 0)
                return new[] { query.ById(criteria.RevisionId.Value) }.AsQueryable();

            // apply current, archived, and deleted switches
            if (criteria.IsCurrent.HasValue)
                query = query.Where(e => e.IsCurrent == criteria.IsCurrent.Value);
            if (criteria.IsArchived.HasValue)
                query = query.Where(e => e.IsArchived == criteria.IsArchived.Value);
            if (criteria.IsDeleted.HasValue)
                query = query.Where(e => e.IsDeleted == criteria.IsDeleted.Value);

            // apply entity id
            if (criteria.EntityId.HasValue && criteria.EntityId != Guid.Empty)
                query = query.Where(e => e.EntityId == criteria.EntityId);

            // apply included revision ids
            if (criteria.RevisionIds != null && criteria.RevisionIds.Count > 0)
                query = query.Where(e => criteria.RevisionIds.Contains(e.RevisionId));

            // apply excluded revision ids
            if (criteria.ExcludeRevisionIds != null && criteria.ExcludeRevisionIds.Count > 0)
                query = query.Where(e => !criteria.ExcludeRevisionIds.Contains(e.RevisionId));

            // apply included entity ids
            if (criteria.EntityIds != null && criteria.EntityIds.Count > 0)
                query = query.Where(e => criteria.EntityIds.Contains(e.EntityId));

            // apply excluded entity ids
            if (criteria.ExcludeEntityIds != null && criteria.ExcludeEntityIds.Count > 0)
                query = query.Where(e => !criteria.ExcludeEntityIds.Contains(e.EntityId));

            return query;
        }
    }
}