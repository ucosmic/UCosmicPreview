//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;

//namespace UCosmic.Domain
//{
//    public abstract class RevisableEntityFacade<TRevisableEntity> where TRevisableEntity : RevisableEntity
//    {
//        protected readonly ICommandEntities Entities;

//        // ReSharper disable UnusedMember.Global
//        internal RevisableEntityFacade()
//        // ReSharper restore UnusedMember.Global
//        {
//        }

//        protected RevisableEntityFacade(ICommandEntities entities)
//        {
//            Entities = entities;
//        }

//        protected IQueryable<TRevisableEntity> EagerLoad(IQueryable<TRevisableEntity> query,
//            params Expression<Func<TRevisableEntity, object>>[] eagerLoads)
//        {
//            if (eagerLoads != null)
//                query = eagerLoads.Aggregate(query, (current, eagerLoad) =>
//                    Entities.EagerLoad(current, eagerLoad));
//            return query;
//        }

//        protected IEnumerable<TRevisableEntity> Get(IQueryable<TRevisableEntity> query, params Expression<Func<TRevisableEntity, object>>[] eagerLoads)
//        {
//            query = EagerLoad(query, eagerLoads);
//            return query;
//        }

//        protected TRevisableEntity Get(IQueryable<TRevisableEntity> query, Guid entityId, params Expression<Func<TRevisableEntity, object>>[] eagerLoads)
//        {
//            query = EagerLoad(query, eagerLoads);
//            var entity = query.By(entityId);
//            return entity;
//        }

//        protected TRevisableEntity Get(IQueryable<TRevisableEntity> query, int revisionId, params Expression<Func<TRevisableEntity, object>>[] eagerLoads)
//        {
//            query = EagerLoad(query, eagerLoads);
//            var entity = query.By(revisionId);
//            return entity;
//        }
//    }
//}
