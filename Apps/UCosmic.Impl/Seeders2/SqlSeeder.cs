using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;

namespace UCosmic.Impl.Seeders2
{
    public abstract class SqlSeeder : BaseDataSeeder
    {
        private readonly DbContext _dbContext;

        protected SqlSeeder(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected abstract IEnumerable<string> Files { get; }

        public override void Seed()
        {
            // get location of consuming application
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (string.IsNullOrWhiteSpace(path))
                throw new InvalidOperationException("UCosmic cannot determine bin path.");
            path = path.Replace("file:\\", string.Empty);

            // this assembly is in bin folder, so go up 1 level
            var mvcDirectory = Directory.GetParent(path);
            var startupDirectory = mvcDirectory.EnumerateDirectories().Single(d => d.Name == "startup");
            var sqlDirectory = startupDirectory.EnumerateDirectories().Single(d => d.Name == "sql");
            var files = sqlDirectory.EnumerateFiles().ToList();
            foreach (var sqlScript in Files)
            {
                var file = files.SingleOrDefault(f => f.Name == sqlScript);
                if (file == null)
                    throw new InvalidOperationException(string.Format("Unable to locate file '{0}' in startup folder.", sqlScript));
                var sql = file.OpenText().ReadToEnd();
                _dbContext.Database.ExecuteSqlCommand(sql);
            }
        }
    }
}
