using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Domain
{
    public class PagedResult<TEntity> : IEnumerable<TEntity> where TEntity : Entity
    {
        private PagedResult() { }

        public PagedResult(IQueryable<TEntity> queryable, PagerOptions options)
        {
            TotalResults = queryable.Count();
            PagerOptions = options;

            if (PagerOptions.PageIndex > 0)
                queryable = queryable.Skip(PagerOptions.PageIndex * PagerOptions.PageSize);

            if (PagerOptions.PageSize > 0)
                queryable = queryable.Take(PagerOptions.PageSize);

            ResultsCollection = queryable.ToList();
        }

        private PagerOptions PagerOptions { get; set; }
        private ICollection<TEntity> ResultsCollection { get; set; }
        public IEnumerable<TEntity> Results { get { return ResultsCollection; } }
        public int ResultCount { get { return ResultsCollection.Count; } }
        public int PageNumber { get { return PagerOptions.PageNumber; } }
        public int PageIndex { get { return PagerOptions.PageIndex; } }
        public int TotalResults { get; private set; }
        public int PageSize { get { return PagerOptions.PageSize; } }
        public int PageCount { get { return (int)Math.Ceiling(TotalResults / (double)PageSize); } }
        public int FirstNumber { get { return FirstIndex + 1; } }
        public int FirstIndex { get { return PagerOptions.PageIndex * PageSize; } }
        public int LastNumber { get { return LastIndex + 1; } }
        public int LastIndex { get { return FirstIndex + ResultCount; } }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}