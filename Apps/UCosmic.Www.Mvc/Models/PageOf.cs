using System.Collections.Generic;

namespace UCosmic.Www.Mvc.Models
{
    public abstract class PageOf<TModel>
    {
        public IEnumerable<TModel> Results { get; private set; }
        public int TotalResults { get; private set; }
    }
}