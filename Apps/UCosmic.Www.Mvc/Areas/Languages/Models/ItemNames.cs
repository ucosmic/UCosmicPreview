using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using UCosmic.Domain.Languages;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class ItemNames : Collection<NameItem>
    {
    }

    public static class ItemNamesProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<IEnumerable<LanguageName>, IEnumerable<NameItem>>()
                ;
            }
        }
    }
}