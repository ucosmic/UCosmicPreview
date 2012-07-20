using System;

namespace UCosmic.Domain.Places
{
    public class SingleGeoNamesFeatureClass : IDefineQuery<GeoNamesFeatureClass>
    {
        public SingleGeoNamesFeatureClass(string code)
        {
            Code = code;
        }

        public string Code { get; private set; }
    }

    public class SingleGeoNamesFeatureClassHandler : IHandleQueries<SingleGeoNamesFeatureClass, GeoNamesFeatureClass>
    {
        private readonly IQueryEntities _entities;

        public SingleGeoNamesFeatureClassHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public GeoNamesFeatureClass Handle(SingleGeoNamesFeatureClass query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.FindByPrimaryKey<GeoNamesFeatureClass>(query.Code);
            return result;
        }
    }
}
