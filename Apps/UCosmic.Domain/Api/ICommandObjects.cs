namespace UCosmic.Domain
{
    public interface ICommandObjects : IUnitOfWork
    {
        int Insert(object entity, bool saveChanges = false);
        int Update(object entity, bool saveChanges = false);
        int Delete(object entity, bool saveChanges = false);
    }
}
