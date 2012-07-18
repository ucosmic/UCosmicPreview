//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using NGeo.GeoNames;

//namespace UCosmic.Domain.Places
//{
//    public class GeoNamesToponymFinder : EntityFinder<GeoNamesToponym>
//    {
//        private readonly IConsumeGeoNames _geoNames;
//        private readonly IManageConfigurations _config;
//        private readonly ICommandObjects _objectCommander;
//        private ReadOnlyCollection<Country> _geoNamesCountries;

//        public GeoNamesToponymFinder(IQueryEntities entityQueries, ICommandObjects objectCommander, 
//            IConsumeGeoNames geoNames, IManageConfigurations config) : base(entityQueries)
//        {
//            _geoNames = geoNames;
//            _config = config;
//            _objectCommander = objectCommander;
//        }

//        public override ICollection<GeoNamesToponym> FindMany(EntityQueryCriteria<GeoNamesToponym> criteria)
//        {
//            var query = InitializeQuery(EntityQueries.GeoNamesToponyms, criteria);
//            var finder = criteria as GeoNamesToponymQuery ?? new GeoNamesToponymQuery();

//            // apply GeoNameId
//            if (finder.GeoNameId.HasValue)
//                return new[] {query.SingleOrDefault(e => e.GeoNameId == finder.GeoNameId.Value)};

//            // apply is country
//            if (finder.IsCountry.HasValue)
//                query = (finder.IsCountry.Value)
//                    ? query.Where(t => t.AsCountry != null)
//                    : query.Where(t => t.AsCountry == null);

//            query = FinalizeQuery(query, criteria);

//            var results = query.ToList();
//            return results;
//        }

//        public GeoNamesToponym FindOne(int geoNameId)
//        {
//            // first look in the db
//            var toponym = FindOne(GeoNamesToponymBy.GeoNameId(geoNameId));

//            // if not in db, add it
//            if (toponym == null)
//            {
//                // invoke geonames service
//                var geoNamesToponym = _geoNames.Get(geoNameId, _config.GeoNamesUserName);
//                if (geoNamesToponym == null) return null;

//                // convert geonames type to entity
//                toponym = geoNamesToponym.ToEntity();

//                // map parent
//                var geoNamesHierarchy = _geoNames.Hierarchy(geoNameId, _config.GeoNamesUserName, ResultStyle.Short);
//                if (geoNamesHierarchy != null && geoNamesHierarchy.Items.Count > 1)
//                    toponym.Parent = FindOne(geoNamesHierarchy.Items[geoNamesHierarchy.Items.Count - 2].GeoNameId);

//                // ensure no duplicate features or time zones are added to the db
//                toponym.Feature.Class = EntityQueries.FindByPrimaryKey(EntityQueries.GeoNamesFeatureClasses, toponym.Feature.ClassCode) 
//                    ?? toponym.Feature.Class;
//                toponym.Feature = EntityQueries.FindByPrimaryKey(EntityQueries.GeoNamesFeatures, toponym.FeatureCode) 
//                    ?? toponym.Feature;
//                toponym.TimeZone = EntityQueries.FindByPrimaryKey(EntityQueries.GeoNamesTimeZones, toponym.TimeZoneId) 
//                    ?? toponym.TimeZone;

//                // map country
//                _geoNamesCountries = _geoNamesCountries ?? _geoNames.Countries(_config.GeoNamesUserName);
//                var geoNamesCountry = _geoNamesCountries.SingleOrDefault(c => c.GeoNameId == geoNameId);
//                if (geoNamesCountry != null)
//                    toponym.AsCountry = geoNamesCountry.ToEntity();

//                // map ancestors
//                DeriveNodes(toponym);

//                // add to db and save
//                _objectCommander.Insert(toponym, true);
//            }

//            return toponym;
//        }

//        private void DeriveNodes(GeoNamesToponym toponym)
//        {
//            toponym.Ancestors = toponym.Ancestors ?? new Collection<GeoNamesToponymNode>();
//            toponym.Offspring = toponym.Offspring ?? new Collection<GeoNamesToponymNode>();

//            toponym.Ancestors.ToList().ForEach(node => 
//                _objectCommander.Delete(node));

//            var separation = 1;
//            var parent = toponym.Parent;
//            while (parent != null)
//            {
//                toponym.Ancestors.Add(new GeoNamesToponymNode
//                {
//                    Ancestor = parent,
//                    Separation = separation++,
//                });
//                parent = parent.Parent;
//            }
//        }
//    }
//}