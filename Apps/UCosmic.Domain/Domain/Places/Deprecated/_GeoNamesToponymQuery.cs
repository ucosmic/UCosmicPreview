//namespace UCosmic.Domain.Places
//{
//    public class GeoNamesToponymQuery : EntityQueryCriteria<GeoNamesToponym>
//    {
//        public int? GeoNameId { get; set; }
//        public bool? IsCountry { get; set; }
//    }

//    public static class GeoNamesToponymBy
//    {
//        public static GeoNamesToponymQuery GeoNameId(int geoNameId)
//        {
//            return new GeoNamesToponymQuery { GeoNameId = geoNameId };
//        }
//    }

//    public static class GeoNamesToponymsWith
//    {
//        public static GeoNamesToponymQuery DefaultCriteria()
//        {
//            return new GeoNamesToponymQuery();
//        }

//        public static GeoNamesToponymQuery IsCountry(bool isCountry)
//        {
//            return new GeoNamesToponymQuery { IsCountry = isCountry };
//        }
//    }
//}