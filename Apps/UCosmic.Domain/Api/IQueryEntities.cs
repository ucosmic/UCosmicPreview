using System;
using System.Linq;
using System.Linq.Expressions;
using UCosmic.Domain;

namespace UCosmic
{
    public interface IQueryEntities
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : Entity;

        TEntity FindByPrimaryKey<TEntity>(params object[] primaryKeyValues)
            where TEntity : Entity;

        IQueryable<TEntity> EagerLoad<TEntity>(IQueryable<TEntity> query, Expression<Func<TEntity, object>> expression)
            where TEntity : Entity;
    }
}
