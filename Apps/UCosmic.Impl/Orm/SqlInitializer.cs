namespace UCosmic.Orm
{
    public static class SqlInitializer
    {
        public static void Seed(UCosmicContext context)
        {
            // index on Language_TwoLetterIsoCode
            context.Database.ExecuteSqlCommand("CREATE UNIQUE NONCLUSTERED INDEX [Language_TwoLetterIsoCode] ON [Languages].[Language] ( [TwoLetterIsoCode] ASC ) ");

            // index on Place_OfficialName
            context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX [Place_OfficialName] ON [Places].[Place] ( [OfficialName] ASC ) ");

            // index on PlaceName_TranslationToLanguage_Text
            context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX [PlaceName_TranslationToLanguage_Text] ON [Places].[PlaceName] ( [TranslationToLanguageId] ASC, [Text] ASC )");

            // index on PlaceName_TranslationToLanguage_AsciiEquivalent
            context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX [PlaceName_TranslationToLanguage_AsciiEquivalent] ON [Places].[PlaceName] ( [TranslationToLanguageId] ASC, [AsciiEquivalent] ASC )");

            context.SaveChanges();
        }
    }
}