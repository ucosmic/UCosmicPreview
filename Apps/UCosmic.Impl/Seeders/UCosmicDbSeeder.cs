using UCosmic.Orm;

namespace UCosmic.Seeders
{
    public abstract class UCosmicDbSeeder : ISeedDb
    {
        protected UCosmicContext Context;

        public abstract void Seed(UCosmicContext context);
    }
}