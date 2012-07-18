using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain.Places
{
    public class GeoNamesFeatureClass : Entity
    {
        public string Code { get; set; }
        
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Code, Name);
        }

    }

    public enum GeoNamesFeatureClassEnum
    {
        AdministrativeBoundary = 0,
        Hydrographic = 1,
        Area = 2,
        PopulatedPlace = 3,
        Road = 4,
        Spot = 5,
        Hypsographic = 6,
        Undersea = 7,
        Vegetation = 8,
    }

    public static class FeatureClassExtensions
    {
        public static string GetCode(this GeoNamesFeatureClassEnum geoNamesFeatureClassEnum)
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

        public static List<string> GetCodes(this IEnumerable<GeoNamesFeatureClassEnum> featureClassEnums)
        {
            return featureClassEnums == null 
                ? null 
                : featureClassEnums.Select(featureClassEnum => featureClassEnum.GetCode()).ToList();
        }
    }

}