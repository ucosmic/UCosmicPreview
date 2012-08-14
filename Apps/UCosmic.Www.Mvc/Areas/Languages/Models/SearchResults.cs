using UCosmic.Www.Mvc.Models;
using AutoMapper;
using UCosmic.Domain.Languages;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class SearchResults : PageOf<SearchResult>
    {
    }

    public static class SearchResultsProfiler
    {
        public class PagedEntitiesToModel : Profile
        {
            protected override void Configure()
            {
                CreateMap<PagedResult<Language>, SearchResults>();
            }
        }
    }
}