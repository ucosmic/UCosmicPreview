using System.Collections.Generic;
using System.Linq;
using UCosmic.Domain.Languages;
using UCosmic.Impl.Orm;

namespace UCosmic.Impl.Seeders
{
    public class CoreDataSqlSeeder : NonContentFileSqlDbSeeder
    {
        protected override IEnumerable<string> SqlScripts
        {
            get
            {
                return new[]
                {
                    "CoreData.sql",
                };
            }
        }

        public override void Seed(UCosmicContext context)
        {
            if (!context.Set<Language>().Any())
                base.Seed(context);
        }
    }
}
