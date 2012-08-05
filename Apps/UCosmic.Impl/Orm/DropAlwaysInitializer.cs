using System.Data.Entity;

namespace UCosmic.Impl.Orm
{
    public class DropAlwaysInitializer : DropCreateDatabaseAlways<UCosmicContext>
    {
        protected override void Seed(UCosmicContext context)
        {
            SqlInitializer.Seed(context);
        }
    }
}