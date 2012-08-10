using UCosmic.Www.Mvc.Models;
using AutoMapper;
using UCosmic.Domain.Languages;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class LanguageResults : PageOf<LanguageResult>
    {
        public string Keyword { get; set; }
    }

    public static class LanguageResultsProfiler
    {
        public class EntitiesToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<PagedResult<Language>, LanguageResults>()
                    .ForMember(d => d.Keyword, o => o.Ignore())
                ;
            }
        }
    }
}