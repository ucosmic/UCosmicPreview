//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using UCosmic.Impl.Orm;

//namespace UCosmic.Impl.Seeders
//{
//    public abstract class NonContentFileSqlDbSeeder : UCosmicDbSeeder
//    {
//        protected abstract IEnumerable<string> SqlScripts { get; }

//        public override void Seed(UCosmicContext context)
//        {
//            Context = context;

//            // get location of consuming application
//            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
//            if (string.IsNullOrWhiteSpace(path))
//                throw new InvalidOperationException("UCosmic cannot determine bin path.");
//            path = path.Replace("file:\\", string.Empty);

//            // this assembly is in bin folder, so go up 1 level
//            var mvcDirectory = Directory.GetParent(path);
//            var startupDirectory = mvcDirectory.EnumerateDirectories().Single(d => d.Name == "startup");
//            var sqlDirectory = startupDirectory.EnumerateDirectories().Single(d => d.Name == "sql");
//            var files = sqlDirectory.EnumerateFiles().ToList();
//            foreach (var sqlScript in SqlScripts)
//            {
//                var file = files.SingleOrDefault(f => f.Name == sqlScript);
//                if (file == null)
//                    throw new InvalidOperationException(string.Format("Unable to locate file '{0}' in startup folder.", sqlScript));
//                var sql = file.OpenText().ReadToEnd();
//                context.Database.ExecuteSqlCommand(sql);
//            }
//        }
//    }
//}