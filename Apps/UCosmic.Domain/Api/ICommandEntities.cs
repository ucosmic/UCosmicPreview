using UCosmic.Domain;

namespace UCosmic
{
    public interface ICommandEntities : IQueryEntities
    {
        void Create<TEntity>(TEntity entity) where TEntity : Entity;
        void Update<TEntity>(TEntity entity) where TEntity : Entity;
        void Purge<TEntity>(TEntity entity) where TEntity : Entity;
    }
}
