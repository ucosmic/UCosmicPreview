//namespace UCosmic.Domain.Places
//{
//    public class PlaceQuery : RevisableEntityQueryCriteria<Place>
//    {
//        public int? WoeId { get; set; }
//        public int? GeoNameId { get; set; }
//        public string AutoCompleteTerm { get; set; }
//    }

//    public static class PlaceBy
//    {
//        public static PlaceQuery WoeId(int woeId)
//        {
//            return new PlaceQuery { WoeId = woeId };
//        }

//        public static PlaceQuery GeoNameId(int geoNameId)
//        {
//            return new PlaceQuery { GeoNameId = geoNameId };
//        }
//    }

//    public static class PlacesWith
//    {
//        public static PlaceQuery AutoCompleteTerm(string keyword, int? maxResults = null)
//        {
//            return new PlaceQuery
//            {
//                AutoCompleteTerm = keyword,
//                MaxResults = maxResults,
//            };
//        }
//    }
//}