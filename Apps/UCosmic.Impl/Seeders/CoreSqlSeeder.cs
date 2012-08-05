using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UCosmic.Domain.Languages;

namespace UCosmic.Impl.Seeders
{
    public class CoreSqlSeeder : SqlSeeder
    {
        private readonly IQueryEntities _entities;

        public CoreSqlSeeder(IQueryEntities entities)
            : base(entities as DbContext)
        {
            _entities = entities;
        }

        protected override IEnumerable<string> Files
        {
            get
            {
                return new[]
                {
                    "CoreData.sql",
                };
            }
        }

        public override void Seed()
        {
            if (!_entities.Query<Language>().Any())
                base.Seed();
        }
    }
}
