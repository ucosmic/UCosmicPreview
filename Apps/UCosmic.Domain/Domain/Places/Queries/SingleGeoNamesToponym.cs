using System;
using System.Collections.ObjectModel;
using System.Linq;
using NGeo.GeoNames;

namespace UCosmic.Domain.Places
{
    public class SingleGeoNamesToponym : IDefineQuery<GeoNamesToponym>
    {
        public SingleGeoNamesToponym(int geoNameId)
        {
            GeoNameId = geoNameId;
        }

        public int GeoNameId { get; private set; }
    }

    public class SingleGeoNamesToponymHandler : IHandleQueries<SingleGeoNamesToponym, GeoNamesToponym>
    {
        private readonly ICommandEntities _entities;
        private readonly IContainGeoNames _geoNames;
        private ReadOnlyCollection<Country> _geoNamesCountries;

        public SingleGeoNamesToponymHandler(ICommandEntities entities
            , IContainGeoNames geoNames
        )
        {
            _entities = entities;
            _geoNames = geoNames;
        }

        public GeoNamesToponym Handle(SingleGeoNamesToponym query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // first look in the db
            var toponym = _entities.FindByPrimaryKey<GeoNamesToponym>(query.GeoNameId);
            if (toponym != null) return toponym;

            // invoke geonames service
            var geoNamesToponym = _geoNames.Get(query.GeoNameId);
            if (geoNamesToponym == null) return null;

            // convert geonames type to entity
            toponym = geoNamesToponym.ToEntity();

            // map parent
            var geoNamesHierarchy = _geoNames.Hierarchy(query.GeoNameId, ResultStyle.Short);
            if (geoNamesHierarchy != null && geoNamesHierarchy.Items.Count > 1)
                toponym.Parent = Handle(new SingleGeoNamesToponym(
                    geoNamesHierarchy.Items[geoNamesHierarchy.Items.Count - 2].GeoNameId));

            //// ensure no duplicate features or time zones are added to the db
            toponym.Feature.Class = new SingleGeoNamesFeatureClassHandler(_entities)
                .Handle(new SingleGeoNamesFeatureClass(toponym.Feature.ClassCode))
                ?? toponym.Feature.Class;

            toponym.Feature = new SingleGeoNamesFeatureHandler(_entities)
                .Handle(new SingleGeoNamesFeature(toponym.FeatureCode))
                ?? toponym.Feature;

            toponym.TimeZone = new SingleGeoNamesTimeZoneHandler(_entities)
                .Handle(new SingleGeoNamesTimeZone(toponym.TimeZoneId))
                ?? toponym.TimeZone;

            // map country
            _geoNamesCountries = _geoNamesCountries ?? _geoNames.Countries();
            var geoNamesCountry = _geoNamesCountries.SingleOrDefault(c => c.GeoNameId == query.GeoNameId);
            if (geoNamesCountry != null)
                toponym.AsCountry = geoNamesCountry.ToEntity();

            // map ancestors
            DeriveNodes(toponym);

            // add to db and save
            _entities.Create(toponym);

            return toponym;
        }

        private void DeriveNodes(GeoNamesToponym toponym)
        {
            toponym.Ancestors = toponym.Ancestors ?? new Collection<GeoNamesToponymNode>();
            toponym.Offspring = toponym.Offspring ?? new Collection<GeoNamesToponymNode>();

            toponym.Ancestors.ToList().ForEach(node =>
                _entities.Purge(node));

            var separation = 1;
            var parent = toponym.Parent;
            while (parent != null)
            {
                toponym.Ancestors.Add(new GeoNamesToponymNode
                {
                    Ancestor = parent,
                    Separation = separation++,
                });
                parent = parent.Parent;
            }
        }
    }
}
