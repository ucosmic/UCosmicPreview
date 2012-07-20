//using System.Collections.Generic;
//using System.Linq;

//namespace UCosmic.Domain
//{
//    public abstract class EntityFinder<TEntity> where TEntity : Entity
//    {
//        protected IQueryEntities EntityQueries { get; private set; }

//        protected EntityFinder(IQueryEntities entityQueries)
//        {
//            EntityQueries = entityQueries;
//        }

//        public abstract ICollection<TEntity> FindMany(EntityQueryCriteria<TEntity> criteria);
//        public virtual TEntity FindOne(EntityQueryCriteria<TEntity> criteria)
//        {
//            return FindMany(criteria).SingleOrDefault();
//        }

//        protected IQueryable<TEntity> InitializeQuery(IQueryable<TEntity> query, EntityQueryCriteria<TEntity> criteria)
//        {
//            // apply context tracking
//            if (!criteria.IsForInsertOrUpdate)
//                query = EntityQueries.ApplyInsertOrUpdate(query, criteria);

//            // apply eager loading
//            query = EntityQueries.ApplyEagerLoading(query, criteria);

//            return query;
//        }

//        protected IQueryable<TEntity> FinalizeQuery(IQueryable<TEntity> query, EntityQueryCriteria<TEntity> criteria)
//        {
//            // apply order by
//            if (criteria.ToBeOrderedBy != null && criteria.ToBeOrderedBy.Count > 0)
//            {
//                var firstOrderBy = criteria.ToBeOrderedBy.First();
//                query = firstOrderBy.Value
//                            ? query.OrderBy(firstOrderBy.Key)
//                            : query.OrderByDescending(firstOrderBy.Key);

//                query = criteria.ToBeOrderedBy.Skip(1).Aggregate(query, (lastOrderBy, nextOrderBy)
//                    => (nextOrderBy.Value)
//                        ? ((IOrderedQueryable<TEntity>)lastOrderBy).ThenBy(nextOrderBy.Key)
//                        : ((IOrderedQueryable<TEntity>)lastOrderBy).ThenByDescending(nextOrderBy.Key));
//            }

//            // apply max results
//            if (criteria.MaxResults.HasValue && criteria.MaxResults >= 0)
//                query = query.Take(criteria.MaxResults.Value);


//            return query;
//        }
//    }
//}
