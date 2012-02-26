using System.Data.Entity;

namespace UCosmic.Orm
{
    public class BrownfieldInitializer : IDatabaseInitializer<UCosmicContext>
    {
        public void InitializeDatabase(UCosmicContext context)
        {
            // do nothing to initialize the database
        }
    }

    public class DropOnModelChangeInitializer : DropCreateDatabaseIfModelChanges<UCosmicContext>
    {
        protected override void Seed(UCosmicContext context)
        {
            SqlInitializer.Seed(context);
        }
    }

    public class DropAlwaysInitializer : DropCreateDatabaseAlways<UCosmicContext>
    {
        protected override void Seed(UCosmicContext context)
        {
            SqlInitializer.Seed(context);
        }
    }

    public class CreateInitializer : CreateDatabaseIfNotExists<UCosmicContext>
    {
        protected override void Seed(UCosmicContext context)
        {
            SqlInitializer.Seed(context);
        }
    }

    public class SqlInitializer
    {
        public static void Seed(UCosmicContext context)
        {
            // unique constraint to enforce 1-1 relationship between InstitutionalAgreement and InstitutionalAgreementConfiguration
            //context.Database.ExecuteSqlCommand("ALTER TABLE [InstitutionalAgreements].[InstitutionalAgreementConfiguration] ADD CONSTRAINT [UC_InstitutionalAgreementConfiguration_ForEstablishmentId] UNIQUE([ForEstablishmentId])");

            context.SaveChanges();
        }
    }
}