using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain
{
    public class RevisableEntityQueryCriteria<TEntity> : EntityQueryCriteria<TEntity> where TEntity : RevisableEntity
    {
        public RevisableEntityQueryCriteria()
        {
            IsCurrent = true;
            IsArchived = false;
            IsDeleted = false;
        }

        public bool? IsCurrent { get; set; }
        public bool? IsArchived { get; set; }
        public bool? IsDeleted { get; set; }

        public int? RevisionId { get; set; }
        public ICollection<int> RevisionIds { get; set; }
        public ICollection<int> ExcludeRevisionIds { get; set; }
        public Guid? EntityId { get; set; }
        public ICollection<Guid> EntityIds { get; set; }
        public ICollection<Guid> ExcludeEntityIds { get; set; }

        public new RevisableEntityQueryCriteria<TEntity> EagerLoad(Expression<Func<TEntity, object>> expression)
        {
            return (RevisableEntityQueryCriteria<TEntity>)base.EagerLoad(expression);
        }

        public new RevisableEntityQueryCriteria<TEntity> ForInsertOrUpdate(bool value = true)
        {
            return (RevisableEntityQueryCriteria<TEntity>)base.ForInsertOrUpdate(value);
        }

        public new RevisableEntityQueryCriteria<TEntity> OrderBy(Expression<Func<TEntity, object>> expression)
        {
            return (RevisableEntityQueryCriteria<TEntity>)base.OrderBy(expression);
        }

        public new RevisableEntityQueryCriteria<TEntity> OrderByDescending(Expression<Func<TEntity, object>> expression)
        {
            return (RevisableEntityQueryCriteria<TEntity>)base.OrderByDescending(expression);
        }
    }

    public static class By<TEntity> where TEntity : RevisableEntity
    {
        public static RevisableEntityQueryCriteria<TEntity> RevisionId(int revisionId)
        {
            return new RevisableEntityQueryCriteria<TEntity> { RevisionId = revisionId };
        }

        public static RevisableEntityQueryCriteria<TEntity> EntityId(Guid entityId)
        {
            return new RevisableEntityQueryCriteria<TEntity> { EntityId = entityId };
        }
    }

    public static class With<TEntity> where TEntity : RevisableEntity
    {
        public static RevisableEntityQueryCriteria<TEntity> DefaultCriteria()
        {
            return new RevisableEntityQueryCriteria<TEntity>();
        }

        public static RevisableEntityQueryCriteria<TEntity> RevisionIds(ICollection<int> revisionIds)
        {
            return new RevisableEntityQueryCriteria<TEntity> { RevisionIds = revisionIds };
        }

        public static RevisableEntityQueryCriteria<TEntity> EntityIds(ICollection<Guid> entityIds)
        {
            return new RevisableEntityQueryCriteria<TEntity> { EntityIds = entityIds };
        }
    }
}