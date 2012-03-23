using System.Collections.Generic;
using System.Linq;
using UCosmic.Orm;

namespace UCosmic.Seeders
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class DevelopmentDataSqlSeeder : NonContentFileSqlDbSeeder
    // ReSharper restore ClassNeverInstantiated.Global
    {

        protected override IEnumerable<string> SqlScripts
        {
            get { return new[] { "DevelopmentData.sql" }; }
        }

        public override void Seed(UCosmicContext context)
        {
            if (!context.Languages.Any())
                base.Seed(context);
        }
    }
}
