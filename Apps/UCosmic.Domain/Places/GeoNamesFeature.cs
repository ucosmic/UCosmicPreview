using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Places
{
    public class GeoNamesFeature : Entity
    {
        public string Code { get; set; }

        public string ClassCode { get; set; }

        public virtual GeoNamesFeatureClass Class { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}:{2}", ClassCode, Code, Name);
        }
    }

    public enum GeoNamesFeatureEnum
    {
        Continent = 0,
        AdministrativeDivisionLevel1 = 1,
        AdministrativeDivisionLevel2 = 2,
        AdministrativeDivisionLevel3 = 3,
        AdministrativeDivisionLevel4 = 4,
        AdministrativeDivisionLevelUndifferentiated = 5,
    }

    public static class FeatureExtensions
    {
        public static string GetCode(this GeoNamesFeatureEnum geoNamesFeatureEnum)
        {
            // http://www.geonames.org/export/codes.html
            switch (geoNamesFeatureEnum)
            {
                case GeoNamesFeatureEnum.Continent: return "CONT";
                case GeoNamesFeatureEnum.AdministrativeDivisionLevel1: return "ADM1";
                case GeoNamesFeatureEnum.AdministrativeDivisionLevel2: return "ADM2";
                case GeoNamesFeatureEnum.AdministrativeDivisionLevel3: return "ADM3";
                case GeoNamesFeatureEnum.AdministrativeDivisionLevel4: return "ADM4";
                case GeoNamesFeatureEnum.AdministrativeDivisionLevelUndifferentiated: return "ADMD";
            }

            throw new InvalidOperationException("FeatureEnum could not be converted to a string.");
        }

        public static List<string> GetCodes(this IEnumerable<GeoNamesFeatureEnum> featureEnums)
        {
            return featureEnums == null 
                ? null 
                : featureEnums.Select(featureEnum => featureEnum.GetCode()).ToList();
        }
    }

}