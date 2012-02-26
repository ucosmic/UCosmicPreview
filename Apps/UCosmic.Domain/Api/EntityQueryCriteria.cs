using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace UCosmic.Domain
{
    public abstract class EntityQueryCriteria<TEntity>
    {
        protected EntityQueryCriteria()
        {
            IsForInsertOrUpdate = false;
            _eagerLoadExpressions = new List<Expression<Func<TEntity, object>>>();
            ToBeOrderedBy = new Dictionary<Expression<Func<TEntity, object>>, bool>();
        }

        public int? MaxResults { get; set; }

        #region Eager Loading

        private readonly List<Expression<Func<TEntity, object>>> _eagerLoadExpressions;

        public ReadOnlyCollection<Expression<Func<TEntity, object>>> ToBeEagerLoaded
        {
            get { return (_eagerLoadExpressions != null) ? _eagerLoadExpressions.AsReadOnly() : null; }
        }

        public EntityQueryCriteria<TEntity> EagerLoad(Expression<Func<TEntity, object>> expression)
        {
            if (expression != null)
                _eagerLoadExpressions.Add(expression);
            return this;
        }

        #endregion
        #region For Insert & Update

        public bool IsForInsertOrUpdate { get; private set; }

        public EntityQueryCriteria<TEntity> ForInsertOrUpdate(bool value = true)
        {
            IsForInsertOrUpdate = value;
            return this;
        }

        #endregion
        #region OrderBy

        public Dictionary<Expression<Func<TEntity, object>>, bool> ToBeOrderedBy { get; private set; }

        public EntityQueryCriteria<TEntity> OrderBy(Expression<Func<TEntity, object>> expression)
        {
            return OrderBy(expression, true);
        }

        public EntityQueryCriteria<TEntity> OrderByDescending(Expression<Func<TEntity, object>> expression)
        {
            return OrderBy(expression, false);
        }

        private EntityQueryCriteria<TEntity> OrderBy(Expression<Func<TEntity, object>> expression, bool isAscending)
        {
            if (expression != null)
                ToBeOrderedBy.Add(expression, isAscending);
            return this;
        }

        #endregion
    }
}
