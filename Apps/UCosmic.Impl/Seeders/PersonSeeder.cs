using UCosmic.Domain.Establishments;
using UCosmic.Orm;

namespace UCosmic.Seeders
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

                var suny = Context.Establishments.ByWebsiteUrl("www.suny.edu");
                EnsurePerson("Mitch.Leventhal@suny.edu", "Mitch", "Leventhal", suny);
                EnsurePerson("Sally.Crimmins@suny.edu", "Sally", "Crimmins Villela", suny);

                var uc = Context.Establishments.ByWebsiteUrl("www.uc.edu");
                EnsurePerson("Ronald.Cushing@uc.edu;Ronald.Cushing@ucmail.uc.edu;cushinrb@uc.edu;cushinrb@ucmail.uc.edu", "Ron", "Cushing", uc);
                EnsurePerson("Mary.Watkins@uc.edu;Mary.Watkins@ucmail.uc.edu;watkinml@uc.edu;watkinml@ucmail.uc.edu", "Mary", "Watkins", uc);
                EnsurePerson("Daniel.Ludwig@uc.edu;Daniel.Ludwig@ucmail.uc.edu;ludwigd@uc.edu;ludwigd@ucmail.uc.edu", "Dan", "Ludwig", uc);

                var lehigh = Context.Establishments.ByWebsiteUrl("www.lehigh.edu");
                EnsurePerson("Debra.Nyby@lehigh.edu;dhn0@lehigh.edu", "Debra", "Nyby", lehigh);
                EnsurePerson("Gary.Lutz@lehigh.edu;jgl3@lehigh.edu", "Gary", "Lutz", lehigh);
                EnsurePerson("mohamed.el-aasser@lehigh.edu;mse0@lehigh.edu", "Mohamed", "El-Aasser", lehigh);

                var usil = Context.Establishments.ByWebsiteUrl("www.usil.edu.pe");
                EnsurePerson("DBallen@usil.edu.pe", "Dora", "Ballen Uriarte", usil);

                var collegeBoard = Context.Establishments.ByWebsiteUrl("www.collegeboard.org");
                EnsurePerson("chensley@collegeboard.org", "Clay", "Hensley", collegeBoard);

                var terraDotta = Context.Establishments.ByWebsiteUrl("www.terradotta.com");
                EnsurePerson("Brandon@terradotta.com", "Brandon", "Lee", terraDotta);
            }
        }
    }
}