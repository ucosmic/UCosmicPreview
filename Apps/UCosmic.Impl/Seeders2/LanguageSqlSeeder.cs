using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UCosmic.Domain.Languages;

namespace UCosmic.Impl.Seeders2
{
    public class LanguageSqlSeeder : SqlSeeder
    {
        private readonly IQueryEntities _entities;

        public LanguageSqlSeeder(IQueryEntities entities) : base(entities as DbContext)
        {
            _entities = entities;
        }

        protected override IEnumerable<string> Files
        {
            get
            {
                return new[] { "LanguagesData.sql" };
            }
        }

        public override void Seed()
        {
            if (!_entities.Query<Language>().Any())
                base.Seed();
        }

    }
}