using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UCosmic.Domain;

namespace UCosmic
{
    public class PagedResult<TEntity> : IEnumerable<TEntity> where TEntity : Entity
    {
        public PagedResult(IQueryable<TEntity> queryable, PagerOptions options)
        {
            options = options ?? PagerOptions.All;
            TotalItems = queryable.Count();
            PagerOptions = options;

            // whenever the PageCount is greater than the PageNumber, reduce PageNumber, options are out of bounds
            if (PageNumber > PageCount) PagerOptions.PageNumber = PageCount;

            if (PagerOptions.PageIndex > 0)
                queryable = queryable.Skip(PagerOptions.PageIndex * PagerOptions.PageSize);

            if (PagerOptions.PageSize > 0)
                queryable = queryable.Take(PagerOptions.PageSize);

            ItemsCollection = queryable.ToArray();
        }

        private PagerOptions PagerOptions { get; set; }
        private ICollection<TEntity> ItemsCollection { get; set; }
        public IEnumerable<TEntity> Items { get { return ItemsCollection; } }
        public int PageNumber { get { return PagerOptions.PageNumber; } }
        public int PageIndex { get { return PagerOptions.PageIndex; } }
        public int TotalItems { get; private set; }
        public int PageSize { get { return PagerOptions.PageSize; } }
        public int PageCount { get { return (int)Math.Ceiling(TotalItems / (double)PageSize); } }
        public int FirstNumber { get { return FirstIndex + 1; } }
        public int FirstIndex { get { return PagerOptions.PageIndex * PageSize; } }
        public int LastNumber { get { return LastIndex + 1; } }
        public int LastIndex { get { return FirstIndex + ItemsCollection.Count; } }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}