using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Www.Mvc.Models
{
    public abstract class PageOf<TModel>
    {
        public IEnumerable<TModel> Items { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get { return (int)Math.Ceiling(TotalItems / (double)PageSize); } }
        public int FirstNumber { get { return FirstIndex + 1; } }
        public int FirstIndex { get { return PageIndex * PageSize; } }
        public int LastNumber { get { return LastIndex + 1; } }
        public int LastIndex { get { return FirstIndex + Items.Count(); } }
    }
}