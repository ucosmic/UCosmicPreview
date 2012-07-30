using System.Data.Entity;

namespace UCosmic.Impl.Orm
{
    public class CreateInitializer : CreateDatabaseIfNotExists<UCosmicContext>
    {
        protected override void Seed(UCosmicContext context)
        {
            SqlInitializer.Seed(context);
        }
    }
}