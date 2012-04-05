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

            // index on PlaceName_Text
            context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX [PlaceName_Text] ON [Places].[PlaceName] ( [Text] ASC )");

            // index on PlaceName_AsciiEquivalent
            context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX [PlaceName_AsciiEquivalent] ON [Places].[PlaceName] ( [AsciiEquivalent] ASC, [NameForPlaceId] ASC, [TranslationToLanguageId] ASC )");

            context.SaveChanges();
        }
    }
}