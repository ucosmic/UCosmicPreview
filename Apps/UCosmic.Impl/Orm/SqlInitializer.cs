namespace UCosmic.Impl.Orm
{
    public static class SqlInitializer
    {
        public static void Seed(UCosmicContext context)
        {
            //// index on Language_TwoLetterIsoCode
            //context.Database.ExecuteSqlCommand("CREATE UNIQUE NONCLUSTERED INDEX [Language_TwoLetterIsoCode] ON [Languages].[Language] ( [TwoLetterIsoCode] ASC ) ");

            //// index on Place_OfficialName
            //context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX [Place_OfficialName] ON [Places].[Place] ( [OfficialName] ASC ) ");

            //// index on PlaceName_TranslationToLanguage_Text
            //context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX [PlaceName_TranslationToLanguage_Text] ON [Places].[PlaceName] ( [TranslationToLanguageId] ASC, [Text] ASC )");

            //// index on PlaceName_TranslationToLanguage_AsciiEquivalent
            //context.Database.ExecuteSqlCommand("CREATE NONCLUSTERED INDEX [PlaceName_TranslationToLanguage_AsciiEquivalent] ON [Places].[PlaceName] ( [TranslationToLanguageId] ASC, [AsciiEquivalent] ASC )");

            #region Establishment indices & stats

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_Establishment_RevisionId_OfficialName] ON [Establishments].[Establishment]([RevisionId], [OfficialName])");
            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_Establishment_RevisionId_TypeId_OfficialName] ON [Establishments].[Establishment]([RevisionId], [TypeId], [OfficialName])");

            #endregion
            #region EstablishmentLocation indices & stats

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_EstablishmentLocation_RevisionId] ON [Establishments].[EstablishmentLocation]
(
	[RevisionId] ASC
)");
            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentLocation_RevisionId_LatLng_Boxing_Zoom_Etc_1] ON [Establishments].[EstablishmentLocation]([RevisionId], [CenterLatitude], [CenterLongitude], [BoundingBoxNorthLatitude], [BoundingBoxEastLongitude], [BoundingBoxSouthLatitude], [BoundingBoxWestLongitude], [GoogleMapZoomLevel], [EntityId], [CreatedOnUtc], [CreatedByPrincipal], [UpdatedOnUtc])");

            #endregion
            #region EstablishmentName indices & stats
            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_EstablishmentName_ForEstablishmentId_IsOfficialName] ON [Establishments].[EstablishmentName]
(
	[ForEstablishmentId] ASC,
	[IsOfficialName] ASC
)
INCLUDE ( [Text],
[AsciiEquivalent],
[TranslationToLanguageId])");
            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_EstablishmentName_ForEstablishmentId_IsOfficialName_Text_TranslationToLanguageId] ON [Establishments].[EstablishmentName]
(
	[ForEstablishmentId] ASC,
	[IsOfficialName] ASC,
	[Text] ASC,
	[TranslationToLanguageId] ASC
)
INCLUDE ( [RevisionId],
[IsFormerName],
[AsciiEquivalent],
[EntityId],
[CreatedOnUtc],
[CreatedByPrincipal],
[UpdatedOnUtc],
[UpdatedByPrincipal],
[Version],
[IsCurrent],
[IsArchived],
[IsDeleted])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentName_AsciiEquivalent_ForEstablishmentId_TranslationToLanguageId] ON [Establishments].[EstablishmentName]([AsciiEquivalent], [ForEstablishmentId], [TranslationToLanguageId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentName_AsciiEquivalent_TranslationToLanguageID] ON [Establishments].[EstablishmentName]([AsciiEquivalent], [TranslationToLanguageId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentName_ForEstablishmentId_IsOfficialName_Text_TranslationToLanguageId] ON [Establishments].[EstablishmentName]([ForEstablishmentId], [IsOfficialName], [Text], [TranslationToLanguageId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentName_IsOfficialName_Text] ON [Establishments].[EstablishmentName]([IsOfficialName], [Text])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentName_RevisionId_TranslationToLanguageId_Text] ON [Establishments].[EstablishmentName]([RevisionId], [TranslationToLanguageId], [Text])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentName_Text_TranslationToLanguageId] ON [Establishments].[EstablishmentName]([Text], [TranslationToLanguageId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentName_TranslationToLanguageId_ForEstablishmentId_RevisionId_Text] ON [Establishments].[EstablishmentName]([TranslationToLanguageId], [ForEstablishmentId], [RevisionId], [Text])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentName_TranslationToLanguageId_ForEstablishmentId_Text] ON [Establishments].[EstablishmentName]([TranslationToLanguageId], [ForEstablishmentId], [Text])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_EstablishmentName_TranslationToLanguageId_IsOfficialName_Text] ON [Establishments].[EstablishmentName]([TranslationToLanguageId], [IsOfficialName], [Text])");

            #endregion
            #region Language indices & stats
            context.Database.ExecuteSqlCommand(
@"CREATE UNIQUE NONCLUSTERED INDEX [UX_Language_TwoLetterIsoCode] ON [Languages].[Language]
(
	[TwoLetterIsoCode] ASC
)");

            #endregion
            #region GeoNamesToponym indices & stats

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_GeoNamesToponym_PlaceId] ON [Places].[GeoNamesToponym]
(
	[PlaceId] ASC
)");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_GeoNamesToponym_PlaceId_GeoNameId] ON [Places].[GeoNamesToponym]([PlaceId], [GeoNameId])");

            #endregion
            #region GeoPlanetPlace indices & stats

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_GeoPlanetPlace_PlaceId] ON [Places].[GeoPlanetPlace]
(
	[PlaceId] ASC
)");

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_GeoPlanetPlace_PlaceId_TypeCode_WoeId] ON [Places].[GeoPlanetPlace]
(
	[PlaceId] ASC,
	[TypeCode] ASC,
	[WoeId] ASC
)");

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_GeoPlanetPlace_PlaceId_WoeId_TypeCode] ON [Places].[GeoPlanetPlace]
(
	[PlaceId] ASC,
	[WoeId] ASC,
	[TypeCode] ASC
)
INCLUDE ( [ParentWoeId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_GeoPlanetPlace_TypeCode_PlaceId_WoeId] ON [Places].[GeoPlanetPlace]([TypeCode], [PlaceId], [WoeId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_GeoPlanetPlace_WoeId_EnglishName_Uri_LatLng_Boxing_Ranks_Etc_1] ON [Places].[GeoPlanetPlace]([WoeId], [EnglishName], [Uri], [Latitude], [Longitude], [NorthLatitude], [EastLongitude], [SouthLatitude], [WestLongitude], [AreaRank], [PopulationRank], [Postal], [CountryCode], [CountryType])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_GeoPlanetPlace_WoeId_ParentWoeId] ON [Places].[GeoPlanetPlace]([WoeId], [ParentWoeId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_GeoPlanetPlace_WoeId_ParentWoeId_TypeCode_EnglishName_Etc_1] ON [Places].[GeoPlanetPlace]([PlaceId], [WoeId], [ParentWoeId], [TypeCode], [EnglishName], [Uri], [Latitude], [Longitude], [NorthLatitude], [EastLongitude], [SouthLatitude], [WestLongitude], [AreaRank], [PopulationRank], [Postal], [CountryCode])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_GeoPlanetPlace_WoeId_TypeCode_PlaceId_EnglishName_Uri_LatLng_Boxing_Ranks_Etc_1] ON [Places].[GeoPlanetPlace]([WoeId], [TypeCode], [PlaceId], [EnglishName], [Uri], [Latitude], [Longitude], [NorthLatitude], [EastLongitude], [SouthLatitude], [WestLongitude], [AreaRank], [PopulationRank], [Postal], [CountryCode], [CountryType])");

            #endregion
            #region Place indices & stats

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_Place_IsCountry] ON [Places].[Place]
(
	[IsCountry] ASC
)");

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_Place_OfficialName] ON [Places].[Place]
(
	[OfficialName] ASC
)");

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_Place_OfficialName_RevisionId] ON [Places].[Place]
(
	[OfficialName] ASC,
	[RevisionId] ASC
)
INCLUDE ( [IsEarth],
[IsContinent],
[IsCountry],
[IsAdmin1],
[IsAdmin2],
[IsAdmin3],
[Latitude],
[Longitude],
[NorthLatitude],
[EastLongitude],
[SouthLatitude],
[WestLongitude],
[EntityId],
[CreatedOnUtc],
[CreatedByPrincipal],
[UpdatedOnUtc],
[UpdatedByPrincipal],
[Version],
[IsCurrent],
[IsArchived],
[IsDeleted],
[ParentId])");

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_Place_RevisionId] ON [Places].[Place]
(
	[RevisionId] ASC
)
INCLUDE ( [ParentId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_Place_OfficialName_RevisionId] ON [Places].[Place]([RevisionId], [OfficialName])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_Place_RevisionId_ParentId_OfficialName_Ises_LatLng_Boxing_EntityId] ON [Places].[Place]([RevisionId], [ParentId], [OfficialName], [IsEarth], [IsContinent], [IsCountry], [IsAdmin1], [IsAdmin2], [IsAdmin3], [Latitude], [Longitude], [NorthLatitude], [EastLongitude], [SouthLatitude], [WestLongitude], [EntityId])");

            #endregion
            #region PlaceName indices & stats

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_PlaceName_AsciiEquivalent] ON [Places].[PlaceName]
(
	[AsciiEquivalent] ASC,
	[NameForPlaceId] ASC,
	[TranslationToLanguageId] ASC
)");

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_PlaceName_NameForPlaceId_TranslationToLanguageId_Text_AsciiEquivalent] ON [Places].[PlaceName]
(
	[NameForPlaceId] ASC,
	[TranslationToLanguageId] ASC,
	[Text] ASC,
	[AsciiEquivalent] ASC
)
INCLUDE ( [RevisionId],
[TranslationToHint],
[IsPreferredTranslation],
[EntityId],
[CreatedOnUtc],
[CreatedByPrincipal],
[UpdatedOnUtc],
[UpdatedByPrincipal],
[Version],
[IsCurrent],
[IsArchived],
[IsDeleted])");

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_PlaceName_Text] ON [Places].[PlaceName]
(
	[Text] ASC
)");

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_PlaceName_TranslationToLanguage_AsciiEquivalent] ON [Places].[PlaceName]
(
	[TranslationToLanguageId] ASC,
	[AsciiEquivalent] ASC
)");

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_PlaceName_TranslationToLanguage_Text] ON [Places].[PlaceName]
(
	[TranslationToLanguageId] ASC,
	[Text] ASC
)");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_PlaceName_AsciiEquivalent_Text_NameForPlaceId] ON [Places].[PlaceName]([AsciiEquivalent], [Text], [NameForPlaceId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_PlaceName_AsciiEquivalent_Text_TranslationToLanguageId_NameForPlaceId] ON [Places].[PlaceName]([AsciiEquivalent], [Text], [TranslationToLanguageId], [NameForPlaceId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_PlaceName_NameForPlaceId_AsciiEquivalent] ON [Places].[PlaceName]([NameForPlaceId], [AsciiEquivalent])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_PlaceName_RevisionId_TranslationToLanguageId] ON [Places].[PlaceName]([RevisionId], [TranslationToLanguageId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_PlaceName_Text_NameForPlaceId] ON [Places].[PlaceName]([Text], [NameForPlaceId])");

            context.Database.ExecuteSqlCommand(
@"CREATE STATISTICS [ST_PlaceName_TranslationToLanguageId_AsciiEquivalent] ON [Places].[PlaceName]([TranslationToLanguageId], [AsciiEquivalent])");

            #endregion
            #region PlaceNode indices & stats

            context.Database.ExecuteSqlCommand(
@"CREATE NONCLUSTERED INDEX [IX_PlaceNode_OffspringId_AncestorId] ON [Places].[PlaceNode]
(
	[OffspringId] ASC,
	[AncestorId] ASC
)
INCLUDE ( [Separation])");

            #endregion

            //context.Database.ExecuteSqlCommand(
//@"");

            context.SaveChanges();
        }
    }
}