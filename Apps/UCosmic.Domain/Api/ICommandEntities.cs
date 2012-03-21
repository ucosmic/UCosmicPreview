namespace UCosmic.Domain
{
    public interface ICommandEntities : IQueryEntities
    {
        void Create(Entity entity);
        void Update(Entity entity);
        void Purge(Entity entity);
    }
}
