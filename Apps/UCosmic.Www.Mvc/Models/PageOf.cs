using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Models
{
    public abstract class PageOf<TModel> : IEnumerable<TModel>
    {
        protected PageOf(IEnumerable<TModel> items, int totalResults)
        {
            Items = items;
            TotalResults = totalResults;
        }

        public IEnumerable<TModel> Items { get; private set; }

        public int TotalResults { get; private set; }

        public IEnumerator<TModel> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}