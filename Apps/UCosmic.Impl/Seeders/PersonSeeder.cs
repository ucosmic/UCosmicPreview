using System;
using System.Linq;
using UCosmic.Domain.Establishments;
using UCosmic.Impl.Orm;

namespace UCosmic.Impl.Seeders
{
    public class PersonSeeder : ISeedDb
    {
        private static readonly IManageConfigurations WebConfig = new DotNetConfigurationManager();

        public void Seed(UCosmicContext context)
        {
            new PersonPreview1Seeder().Seed(context);
        }

        private class PersonPreview1Seeder : BasePersonSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                var suny = Context.Set<Establishment>()
                    .SingleOrDefault(e => e.WebsiteUrl != null && e.WebsiteUrl.Equals("www.suny.edu", StringComparison.OrdinalIgnoreCase));
                EnsurePerson("Mitch.Leventhal@suny.edu", "Mitch", "Leventhal", suny);
                EnsurePerson("Sally.Crimmins@suny.edu", "Sally", "Crimmins Villela", suny);

                var uc = Context.Set<Establishment>()
                    .SingleOrDefault(e => e.WebsiteUrl != null && e.WebsiteUrl.Equals("www.uc.edu", StringComparison.OrdinalIgnoreCase));
                EnsurePerson("Ronald.Cushing@uc.edu;Ronald.Cushing@ucmail.uc.edu;cushinrb@uc.edu;cushinrb@ucmail.uc.edu", "Ron", "Cushing", uc);
                EnsurePerson("Mary.Watkins@uc.edu;Mary.Watkins@ucmail.uc.edu;watkinml@uc.edu;watkinml@ucmail.uc.edu", "Mary", "Watkins", uc);
                //EnsurePerson("Daniel.Ludwig@uc.edu;Daniel.Ludwig@ucmail.uc.edu;ludwigd@uc.edu;ludwigd@ucmail.uc.edu", "Dan", "Ludwig", uc);

                var lehigh = Context.Set<Establishment>()
                    .SingleOrDefault(e => e.WebsiteUrl != null && e.WebsiteUrl.Equals("www.lehigh.edu", StringComparison.OrdinalIgnoreCase));
                EnsurePerson("Debra.Nyby@lehigh.edu;dhn0@lehigh.edu", "Debra", "Nyby", lehigh);
                EnsurePerson("Gary.Lutz@lehigh.edu;jgl3@lehigh.edu", "Gary", "Lutz", lehigh);
                EnsurePerson("mohamed.el-aasser@lehigh.edu;mse0@lehigh.edu", "Mohamed", "El-Aasser", lehigh);

                var usil = Context.Set<Establishment>()
                    .SingleOrDefault(e => e.WebsiteUrl != null && e.WebsiteUrl.Equals("www.usil.edu.pe", StringComparison.OrdinalIgnoreCase));
                EnsurePerson("DBallen@usil.edu.pe", "Dora", "Ballen Uriarte", usil);

                var collegeBoard = Context.Set<Establishment>()
                    .SingleOrDefault(e => e.WebsiteUrl != null && e.WebsiteUrl.Equals("www.collegeboard.org", StringComparison.OrdinalIgnoreCase));
                EnsurePerson("chensley@collegeboard.org", "Clay", "Hensley", collegeBoard);

                var terraDotta = Context.Set<Establishment>()
                    .SingleOrDefault(e => e.WebsiteUrl != null && e.WebsiteUrl.Equals("www.terradotta.com", StringComparison.OrdinalIgnoreCase));
                EnsurePerson("Brandon@terradotta.com", "Brandon", "Lee", terraDotta);
            }
        }
    }
}