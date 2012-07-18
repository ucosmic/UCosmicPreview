using System.Collections.Generic;

namespace UCosmic.Domain.Places
{
    internal static class KnownWoeIds
    {
        internal static readonly Dictionary<int, int> ToGeoNameIds = new Dictionary<int, int>
        {
            { GeoPlanetPlace.EarthWoeId, GeoNamesToponym.EarthGeoNameId }, // earth
            { 24865670, 6255146 },  // Africa
            { 24865671, 6255147 },  // Asia
            { 24865675, 6255148 },  // Europe
            { 24865672, 6255149 },  // North America
            { 24865673, 6255150 },  // South America
            { 55949070, 6255151 },  // Oceania / Australia
            { 28289421, 6255152 },  // Antarctic(a) / South Pole continent
            { 12577865, 661882 },   // Aland Islands
            { 28289409, 6697173 },  // Antarctica country
            { 23424886, 1024031 },  // Mayotte country
            { 28289413, 607072 },   // Svalbard and Jan Mayen country
            { 23424990, 2461445 },  // Western Sahara country
            { 23424749, 2077507 },  // Ashmore and Cartier Islands
            { 23424790, 2170371 },  // Coral Sea Islands
            { 23424920, 1821073 },  // Paracel Islands
            { 56120896, 7603259 },  // Luxor
            { 23424847, 3042225 },  // Isle of Man
            { 23424788, 4041468 },  // Northern Mariana Islands
        };

    }
}
