using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Places
{
    internal static class GeoNamesFeatureEnumExtensions
    {
        internal static string GetCode(this GeoNamesFeatureEnum geoNamesFeatureEnum)
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

        internal static List<string> GetCodes(this IEnumerable<GeoNamesFeatureEnum> featureEnums)
        {
            return featureEnums == null
                ? null
                : featureEnums.Select(GetCode).ToList();
        }
    }
}