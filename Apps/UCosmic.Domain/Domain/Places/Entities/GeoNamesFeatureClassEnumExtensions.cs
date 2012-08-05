using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Places
{
    internal static class GeoNamesFeatureClassEnumExtensions
    {
        internal static string GetCode(this GeoNamesFeatureClassEnum geoNamesFeatureClassEnum)
        {
            // http://www.geonames.org/source-code/javadoc/org/geonames/FeatureClass.html
            switch (geoNamesFeatureClassEnum)
            {
                case GeoNamesFeatureClassEnum.AdministrativeBoundary: return "A";
                case GeoNamesFeatureClassEnum.Hydrographic:           return "H";
                case GeoNamesFeatureClassEnum.Area:                   return "L";
                case GeoNamesFeatureClassEnum.PopulatedPlace:         return "P";
                case GeoNamesFeatureClassEnum.Road:                   return "R";
                case GeoNamesFeatureClassEnum.Spot:                   return "S";
                case GeoNamesFeatureClassEnum.Hypsographic:           return "T";
                case GeoNamesFeatureClassEnum.Undersea:               return "U";
                case GeoNamesFeatureClassEnum.Vegetation:             return "V";
            }

            throw new InvalidOperationException("FeatureClassEnum could not be converted to a string.");
        }

        internal static List<string> GetCodes(this IEnumerable<GeoNamesFeatureClassEnum> featureClassEnums)
        {
            return featureClassEnums == null
                ? null
                : featureClassEnums.Select(GetCode).ToList();
        }
    }
}