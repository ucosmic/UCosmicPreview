using System.Collections.Generic;
using System.Linq;
using UCosmic.Impl.Orm;
using UCosmic.Domain.Languages;

namespace UCosmic.Impl.Seeders
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class DevelopmentDataSqlSeeder : CoreDataSqlSeeder
    // ReSharper restore ClassNeverInstantiated.Global
    {

        protected override IEnumerable<string> SqlScripts
        {
            get
            {
                var baseScripts = base.SqlScripts.ToList();
                baseScripts.Add("DevelopmentData.sql");
                return baseScripts.ToArray();
            }
        }

        public override void Seed(UCosmicContext context)
        {
            if (!context.Set<Language>().Any())
                base.Seed(context);
        }
    }
}
