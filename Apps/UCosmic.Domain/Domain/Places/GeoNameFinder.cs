using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace UCosmic.Domain.Places
{
    public class GeoNameFinder
    {
        public bool? IsCountry { get; set; }

        private List<Expression<Func<GeoNamesToponym, object>>> _toBeEagerLoaded;
        public ReadOnlyCollection<Expression<Func<GeoNamesToponym, object>>> ToBeEagerLoaded
        {
            get { return (_toBeEagerLoaded != null) ? _toBeEagerLoaded.AsReadOnly() : null; }
        }

        public GeoNameFinder EagerLoad(Expression<Func<GeoNamesToponym, object>> expression)
        {
            if (expression != null)
            {
                if (_toBeEagerLoaded == null)
                    _toBeEagerLoaded = new List<Expression<Func<GeoNamesToponym, object>>>();
                _toBeEagerLoaded.Add(expression);
            }
            return this;
        }

        public bool IsForInsertOrUpdate { get; private set; }

        public GeoNameFinder ForInsertOrUpdate()
        {
            IsForInsertOrUpdate = true;
            return this;
        }
    }
}