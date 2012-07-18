using System;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;

namespace UCosmic.Domain.Places
{
    internal static class QueryPlaces
    {
        internal static IQueryable<Place> WithName(this IQueryable<Place> queryable, string term, StringMatchStrategy matchStrategy)
        {
            var matchesName =
                OfficialNameMatches(term, matchStrategy)
                //.Or
                //(
                //    NonOfficialNameMatches(term, matchStrategy)
                //)
            ;

            return queryable.AsExpandable().Where(matchesName);
        }

        internal static Place ByWoeId(this IQueryable<Place> queryable, int woeId)
        {
            return queryable.SingleOrDefault(p => p.GeoPlanetPlace != null && p.GeoPlanetPlace.WoeId == woeId);
        }

        internal static Place ByGeoNameId(this IQueryable<Place> queryable, int geoNameId)
        {
            return queryable.SingleOrDefault(p => p.GeoNamesToponym != null && p.GeoNamesToponym.GeoNameId == geoNameId);
        }

        private static Expression<Func<Place, bool>> OfficialNameMatches(string term, StringMatchStrategy matchStrategy)
        {
            switch (matchStrategy)
            {
                case StringMatchStrategy.Equals:
                    return place => place.OfficialName.Equals(term);

                case StringMatchStrategy.StartsWith:
                    return place => place.OfficialName.StartsWith(term);

                case StringMatchStrategy.Contains:
                    return place => place.OfficialName.Contains(term);
            }
            throw new NotSupportedException(string.Format("StringMatchStrategy '{0}' is not supported.", matchStrategy));
        }

        private static Expression<Func<Place, bool>> NonOfficialNameMatches(string term, StringMatchStrategy matchStrategy)
        {
            // using this predicate causes the 'name' parameter to not be bound
            // fixed: http://stackoverflow.com/questions/10689506/unable-to-refactor-using-linq-to-entities-and-linqkit-predicatebuilder
            var names = QueryPlaceNames.SearchTermMatches(term, matchStrategy);
            // ReSharper disable ConvertClosureToMethodGroup
            Expression<Func<Place, bool>> places = place => place.Names.Any(name => names.Invoke(name));
            // ReSharper restore ConvertClosureToMethodGroup
            return places.Expand();
        }
    }
}
