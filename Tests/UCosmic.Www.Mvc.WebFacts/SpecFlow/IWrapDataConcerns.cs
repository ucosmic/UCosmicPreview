using UCosmic.Impl.Orm;

namespace UCosmic.Www.Mvc
{
    public interface IWrapDataConcerns
    {
        IQueryEntities Queries { get; }
        ICommandEntities Commands { get; }
        IUnitOfWork UnitOfWork { get; }
    }

    public class DataConcernsWrapper : IWrapDataConcerns
    {
        public DataConcernsWrapper(UCosmicContext dbContext)
        {
            Queries = dbContext;
            Commands = dbContext;
            UnitOfWork = dbContext;
        }

        public IQueryEntities Queries
        {
            get;
            private set;
        }

        public ICommandEntities Commands
        {
            get;
            private set;
        }

        public IUnitOfWork UnitOfWork
        {
            get;
            private set;
        }
    }
}