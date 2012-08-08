using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using UCosmic.Domain.Languages;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class LanguageTable : Collection<LanguageTableRow>
    {
    }

    public static class LanguageTableProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<IEnumerable<Language>, IEnumerable<LanguageTableRow>>();
            }
        }
    }
}