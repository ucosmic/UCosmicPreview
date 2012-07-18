using System;
using System.Collections.ObjectModel;
using System.Linq;
using NGeo.GeoNames;

namespace UCosmic.Domain.Places
{
    public class GetGeoNamesToponymByGeoNameIdQuery : BaseEntityQuery<GeoNamesToponym>, IDefineQuery<GeoNamesToponym>
    {
        public int GeoNameId { get; set; }
    }

    public class GetGeoNamesToponymByGeoNameIdHandler : IHandleQueries<GetGeoNamesToponymByGeoNameIdQuery, GeoNamesToponym>
    {
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContainGeoNames _geoNames;
        private ReadOnlyCollection<Country> _geoNamesCountries;

        public GetGeoNamesToponymByGeoNameIdHandler(ICommandEntities entities, IUnitOfWork unitOfWork, IContainGeoNames geoNames)
        {
            _entities = entities;
            _unitOfWork = unitOfWork;
            _geoNames = geoNames;
        }

        public GeoNamesToponym Handle(GetGeoNamesToponymByGeoNameIdQuery query)
        {
            if (query == null) throw new ArgumentNullException("query");

            // first look in the db
            var toponym = _entities.GeoNamesToponyms
                .EagerLoad(query.EagerLoad, _entities)
                .ByGeoNameId(query.GeoNameId)
            ;
            if (toponym != null) return toponym;

            // invoke geonames service
            //var geoNamesToponym = _geoNames.Get(geoNameId, _config.GeoNamesUserName);
            var geoNamesToponym = _geoNames.Get(query.GeoNameId);
            if (geoNamesToponym == null) return null;

            // convert geonames type to entity
            toponym = geoNamesToponym.ToEntity();

            // map parent
            var geoNamesHierarchy = _geoNames.Hierarchy(query.GeoNameId, ResultStyle.Short);
            if (geoNamesHierarchy != null && geoNamesHierarchy.Items.Count > 1)
                toponym.Parent = Handle(
                    new GetGeoNamesToponymByGeoNameIdQuery
                    {
                        GeoNameId = geoNamesHierarchy.Items[geoNamesHierarchy.Items.Count - 2].GeoNameId,
                    });

            //// ensure no duplicate features or time zones are added to the db
            //toponym.Feature.Class = EntityQueries.FindByPrimaryKey(EntityQueries.GeoNamesFeatureClasses, toponym.Feature.ClassCode)
            //    ?? toponym.Feature.Class;
            toponym.Feature.Class = new GetGeoNamesFeatureClassByCodeHandler(_entities)
                .Handle(
                    new GetGeoNamesFeatureClassByCodeQuery
                    {
                        Code = toponym.Feature.ClassCode
                    })
                ?? toponym.Feature.Class;
            //toponym.Feature = EntityQueries.FindByPrimaryKey(EntityQueries.GeoNamesFeatures, toponym.FeatureCode)
            //    ?? toponym.Feature;
            toponym.Feature = new GetGeoNamesFeatureByCodeHandler(_entities)
                .Handle(
                    new GetGeoNamesFeatureByCodeQuery
                    {
                        Code = toponym.FeatureCode,
                    })
                ?? toponym.Feature;
            //toponym.TimeZone = EntityQueries.FindByPrimaryKey(EntityQueries.GeoNamesTimeZones, toponym.TimeZoneId)
            //    ?? toponym.TimeZone;
            toponym.TimeZone = new GetGeoNamesTimeZoneByIdHandler(_entities)
                .Handle(
                    new GetGeoNamesTimeZoneByIdQuery
                    {
                        TimeZoneId = toponym.TimeZoneId
                    })
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
            _unitOfWork.SaveChanges();

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
