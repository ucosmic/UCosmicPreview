using System.Data.Entity;

namespace UCosmic.Impl.Orm
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class BrownfieldInitializer : IDatabaseInitializer<UCosmicContext>
    // ReSharper restore ClassNeverInstantiated.Global
    {
        public void InitializeDatabase(UCosmicContext context)
        {
            // do nothing to initialize the database
        }
    }
}