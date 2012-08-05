using System.Data.Entity;

namespace UCosmic.Impl.Orm
{
    public class DropOnModelChangeInitializer : DropCreateDatabaseIfModelChanges<UCosmicContext>
    {
        protected override void Seed(UCosmicContext context)
        {
            SqlInitializer.Seed(context);
        }
    }
}