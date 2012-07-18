//using System.Collections.Generic;
//using System.Linq;
//using System.Globalization;

//namespace UCosmic.Domain.Places
//{
//    public class PlaceFinder : RevisableEntityFinder<Place>
//    {
//        public PlaceFinder(IQueryEntities entityQueries)
//            : base(entityQueries)
//        {
//        }

//        public override ICollection<Place> FindMany(RevisableEntityQueryCriteria<Place> criteria)
//        {
//            var query = InitializeQuery(EntityQueries.Places, criteria);
//            var finder = criteria as PlaceQuery ?? new PlaceQuery();

//            // apply geoNameId
//            if (finder.GeoNameId.HasValue)
//                query = query.Where(e => e.GeoNamesToponym != null
//                    && e.GeoNamesToponym.GeoNameId == finder.GeoNameId.Value);

//            // apply woeId
//            if (finder.WoeId.HasValue)
//                query = query.Where(e => e.GeoPlanetPlace != null
//                    && e.GeoPlanetPlace.WoeId == finder.WoeId.Value);

//            // apply auto complete term
//            if (!string.IsNullOrWhiteSpace(finder.AutoCompleteTerm))
//                query = query.Where(e => 
//                    e.OfficialName.StartsWith(finder.AutoCompleteTerm) 
//                    ||
//                    e.Names.Any(n => 
//                        (n.Text.StartsWith(finder.AutoCompleteTerm)
//                            && n.TranslationToLanguage != null
//                            && n.TranslationToLanguage.TwoLetterIsoCode == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
//                        ||
//                        (n.AsciiEquivalent != null && n.AsciiEquivalent.StartsWith(finder.AutoCompleteTerm)
//                            && n.TranslationToLanguage != null
//                            && n.TranslationToLanguage.TwoLetterIsoCode == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
//                    )
//                );

//            query = FinalizeQuery(query, criteria);

//            var results = query.ToList();
//            return results;
//        }
//    }
//}
