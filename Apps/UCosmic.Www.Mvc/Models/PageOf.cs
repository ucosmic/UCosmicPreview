using System;
using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Www.Mvc.Models
{
    public abstract class PageOf<TModel>
    {
        public IEnumerable<TModel> Results { get; set; }
        public int ResultCount { get { return Results.Count(); } }
        public int TotalResults { get; set; }
        public int PageNumber { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get { return (int)Math.Ceiling(TotalResults / (double)PageSize); } }
        public int FirstNumber { get { return FirstIndex + 1; } }
        public int FirstIndex { get { return PageIndex * PageSize; } }
        public int LastNumber { get { return LastIndex + 1; } }
        public int LastIndex { get { return FirstIndex + ResultCount; } }
    }
}