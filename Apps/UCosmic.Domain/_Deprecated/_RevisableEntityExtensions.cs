//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace UCosmic.Domain
//{
//    public static class RevisableEntityExtensions
//    {
//        //#region Current Queryables, with & without EntityId

//        //public static IQueryable<TEntity> Current<TEntity>(this IQueryable<TEntity> query) where TEntity : RevisableEntity
//        //{
//        //    return (query != null) 
//        //               ? query.Where(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted)
//        //               : null;
//        //}

//        //public static TEntity Current<TEntity>(this IQueryable<TEntity> query, Guid entityId) where TEntity : RevisableEntity
//        //{
//        //    return (query != null && entityId != Guid.Empty)
//        //               ? query.Current().SingleOrDefault(e => e.EntityId == entityId)
//        //               : null;
//        //}

//        //#endregion
//        //#region Current Enumerables, with & without EntityId

//        //public static IEnumerable<TEntity> Current<TEntity>(this IEnumerable<TEntity> enumerable) where TEntity : RevisableEntity
//        //{
//        //    return (enumerable != null)
//        //               ? enumerable.Where(e => e.IsCurrent && !e.IsArchived && !e.IsDeleted)
//        //               : Enumerable.Empty<TEntity>();
//        //}

//        //public static TEntity Current<TEntity>(this IEnumerable<TEntity> enumerable, Guid entityId) where TEntity : RevisableEntity
//        //{
//        //    return (enumerable != null && entityId != Guid.Empty)
//        //               ? enumerable.Current().SingleOrDefault(e => e.EntityId == entityId)
//        //               : null;
//        //}

//        //#endregion
//        //#region ById

//        //public static TEntity ById<TEntity>(this IQueryable<TEntity> query, int revisionId) where TEntity : RevisableEntity
//        //{
//        //    return (query != null)
//        //        ? query.SingleOrDefault(e => e.RevisionId == revisionId)
//        //        : null;
//        //}

//        //public static TEntity ById<TEntity>(this IEnumerable<TEntity> enumerable, int revisionId) where TEntity : RevisableEntity
//        //{
//        //    return (enumerable != null)
//        //        ? enumerable.SingleOrDefault(e => e.RevisionId == revisionId)
//        //        : null;
//        //}

//        //public static TEntity ById<TEntity>(this IQueryable<TEntity> query, Guid entityId) where TEntity : RevisableEntity
//        //{
//        //    return (query != null && entityId != Guid.Empty)
//        //        ? query.SingleOrDefault(e => e.EntityId == entityId)
//        //        : null;
//        //}

//        //public static TEntity ById<TEntity>(this IEnumerable<TEntity> enumerable, Guid entityId) where TEntity : RevisableEntity
//        //{
//        //    return (enumerable != null && entityId != Guid.Empty)
//        //        ? enumerable.SingleOrDefault(e => e.EntityId == entityId)
//        //        : null;
//        //}

//        //#endregion

//    }
//}