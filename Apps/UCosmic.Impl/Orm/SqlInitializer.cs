namespace UCosmic.Orm
{
    public static class SqlInitializer
    {
        public static void Seed(UCosmicContext context)
        {
            // unique constraint to enforce 1-1 relationship between InstitutionalAgreement and InstitutionalAgreementConfiguration
            //context.Database.ExecuteSqlCommand("ALTER TABLE [InstitutionalAgreements].[InstitutionalAgreementConfiguration] ADD CONSTRAINT [UC_InstitutionalAgreementConfiguration_ForEstablishmentId] UNIQUE([ForEstablishmentId])");

            context.SaveChanges();
        }
    }
}