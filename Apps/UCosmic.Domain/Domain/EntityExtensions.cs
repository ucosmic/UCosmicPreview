using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UCosmic.Domain
{
    public static class EntityExtensions
    {
        internal static int NextNumber(this IEnumerable<IAmNumbered> enumerable)
        {
            var collection = enumerable.Select(x => x.Number).ToArray();
            var max = collection.Any() ? collection.Max() : 0;
            return ++max;
        }

        private static IQueryable<TEntity> EagerLoad<TEntity>(this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, object>> expression,
            IQueryEntities entities)
            where TEntity : Entity
        {
            return entities.EagerLoad(queryable, expression);
        }

        internal static IQueryable<TEntity> EagerLoad<TEntity>(this IQueryable<TEntity> queryable,
            IEnumerable<Expression<Func<TEntity, object>>> expressions,
            IQueryEntities entities)
            where TEntity : Entity
        {
            if (expressions != null)
                queryable = expressions.Aggregate(queryable, (current, expression) => current.EagerLoad(expression, entities));
            return queryable;
        }

        internal static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> queryable,
            IEnumerable<KeyValuePair<Expression<Func<TEntity, object>>, OrderByDirection>> expressions)
        {
            // http://stackoverflow.com/a/9155222/304832
            if (expressions != null)
            {
                // first expression is passed to OrderBy/Descending, others are passed to ThenBy/Descending
                var counter = 0;
                foreach (var expression in expressions)
                {
                    var unaryExpression = expression.Key.Body as UnaryExpression;

                    if (unaryExpression != null)
                    {
                        var propertyExpression = (MemberExpression)unaryExpression.Operand;
                        var parameters = expression.Key.Parameters;
                        if (propertyExpression.Type == typeof(DateTime))
                        {
                            #region DateTime

                            var dateTimeExpression = Expression.Lambda<Func<TEntity, DateTime>>(propertyExpression, parameters);
                            if (counter < 1)
                            {
                                queryable = expression.Value == OrderByDirection.Ascending
                                    ? queryable.OrderBy(dateTimeExpression)
                                    : queryable.OrderByDescending(dateTimeExpression);
                            }
                            else
                            {
                                queryable = expression.Value == OrderByDirection.Ascending
                                    ? ((IOrderedQueryable<TEntity>)queryable).ThenBy(dateTimeExpression)
                                    : ((IOrderedQueryable<TEntity>)queryable).ThenByDescending(dateTimeExpression);
                            }

                            #endregion
                        }
                        else if (propertyExpression.Type == typeof(int))
                        {
                            #region int

                            var intExpression = Expression.Lambda<Func<TEntity, int>>(propertyExpression, parameters);
                            if (counter < 1)
                            {
                                queryable = expression.Value == OrderByDirection.Ascending
                                    ? queryable.OrderBy(intExpression)
                                    : queryable.OrderByDescending(intExpression);
                            }
                            else
                            {
                                queryable = expression.Value == OrderByDirection.Ascending
                                    ? ((IOrderedQueryable<TEntity>)queryable).ThenBy(intExpression)
                                    : ((IOrderedQueryable<TEntity>)queryable).ThenByDescending(intExpression);
                            }

                            #endregion
                        }
                        else
                        {
                            throw new NotSupportedException("Object type resolution not implemented for this type");
                        }
                    }
                    else
                    {
                        #region object

                        if (counter < 1)
                        {
                            queryable = expression.Value == OrderByDirection.Ascending
                                ? queryable.OrderBy(expression.Key)
                                : queryable.OrderByDescending(expression.Key);
                        }
                        else
                        {
                            queryable = expression.Value == OrderByDirection.Ascending
                                ? ((IOrderedQueryable<TEntity>)queryable).ThenBy(expression.Key)
                                : ((IOrderedQueryable<TEntity>)queryable).ThenByDescending(expression.Key);
                        }

                        #endregion
                    }
                    ++counter;
                }
            }
            return queryable;
        }

        public static TRevisableEntity By<TRevisableEntity>(this IEnumerable<TRevisableEntity> enumerable, Guid entityId)
            where TRevisableEntity : RevisableEntity
        {
            return enumerable.AsQueryable().By(entityId);
        }

        public static TRevisableEntity By<TRevisableEntity>(this IEnumerable<TRevisableEntity> enumerable, int revisionId)
            where TRevisableEntity : RevisableEntity
        {
            return enumerable.AsQueryable().By(revisionId);
        }

        internal static TRevisableEntity By<TRevisableEntity>(this IQueryable<TRevisableEntity> queryable, Guid entityId)
            where TRevisableEntity : RevisableEntity
        {
            if (entityId == Guid.Empty)
                throw new InvalidOperationException(string.Format("EntityId Guid is empty ({0}).", Guid.Empty));
            return queryable.SingleOrDefault(e => e.EntityId == entityId);
        }

        internal static TRevisableEntity By<TRevisableEntity>(this IQueryable<TRevisableEntity> queryable, int revisionId)
            where TRevisableEntity : RevisableEntity
        {
            return queryable.SingleOrDefault(e => e.RevisionId == revisionId);
        }

        internal static IQueryable<TRevisableEntity> Exclude<TRevisableEntity>(this IQueryable<TRevisableEntity> queryable, IEnumerable<Guid> entityIds)
            where TRevisableEntity : RevisableEntity
        {
            if (entityIds != null)
                queryable = queryable.Where(e => !entityIds.Contains(e.EntityId));
            return queryable;
        }
    }
}
