using UCosmic.Domain;

namespace UCosmic
{
    public interface ICommandEntities : IQueryEntities
    {
        void Create(Entity entity);
        void Update(Entity entity);
        void Purge(Entity entity);
    }
}
