using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using UCosmic.Domain.Languages;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class LanguageNameTable : Collection<LanguageNameTableRow>
    {
    }

    public static class LanguageNameTableProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<IEnumerable<LanguageName>, IEnumerable<LanguageNameTableRow>>()
                ;
            }
        }
    }
}