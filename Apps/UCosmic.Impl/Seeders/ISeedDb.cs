using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UCosmic.Orm;

namespace UCosmic.Seeders
{
    public interface ISeedDb
    {
        void Seed(UCosmicContext context);
    }

    // ReSharper disable UnusedMember.Global
    public class BrownfieldDbSeeder : ISeedDb
    // ReSharper restore UnusedMember.Global
    {
        public void Seed(UCosmicContext context)
        {
            // do nothing
        }
    }

    public abstract class UCosmicDbSeeder : ISeedDb
    {
        protected UCosmicContext Context;

        public abstract void Seed(UCosmicContext context);
    }

    //public abstract class EmbeddedResourceSqlDbSeeder : UCosmicDbSeeder
    //{
    //    protected abstract IEnumerable<string> SqlScripts { get; }

    //    public override void Seed(UCosmicContext context)
    //    {
    //        Context = context;

    //        var assembly = Assembly.GetExecutingAssembly();

    //        foreach (var sqlScript in SqlScripts)
    //        {
    //            var resourcePath = string.Format("UCosmic.Seeders.Sql.{0}.sql", sqlScript);
    //            var stream = assembly.GetManifestResourceStream(resourcePath);
    //            if (stream == null) throw new InvalidOperationException(
    //                string.Format("Resource path {0} could not be streamed.", resourcePath));

    //            var reader = new StreamReader(stream);
    //            var sql = reader.ReadToEnd();
    //            Context.Database.ExecuteSqlCommand(sql);
    //        }
    //    }
    //}

    public abstract class NonContentFileSqlDbSeeder : UCosmicDbSeeder
    {
        protected abstract IEnumerable<string> SqlScripts { get; }

        public override void Seed(UCosmicContext context)
        {
            Context = context;

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
            foreach (var sqlScript in SqlScripts)
            {
                var file = files.SingleOrDefault(f => f.Name == sqlScript);
                if (file == null)
                    throw new InvalidOperationException(string.Format("Unable to locate file '{0}' in startup folder.", sqlScript));
                var sql = file.OpenText().ReadToEnd();
                context.Database.ExecuteSqlCommand(sql);
            }
        }
    }

}
