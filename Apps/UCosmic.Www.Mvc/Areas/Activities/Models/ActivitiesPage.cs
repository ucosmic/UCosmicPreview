using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UCosmic.Domain;
using UCosmic.Domain.Activities;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Activities.Models
{
    public class ActivitiesPage : PageOf<ActivitiesPage.Item>
    {
        public ActivitiesPage(IEnumerable<Item> items, int totalResults)
            : base(items, totalResults) { }

        public class Item
        {
            public int Number { get; set; }
            public string Title { get; set; }
            public ActivityMode Mode { get; set; }
            public Tag[] Tags { get; set; }
            public class Tag
            {
                public string Text { get; set; }
            }
        }
    }

    public static class ActivitiesPageProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(ActivitiesPageProfiler));
        }

        private class EntitiesToModelsProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<PagedResult<Activity>, ActivitiesPage>()
                    .ConstructUsing(s =>
                        new ActivitiesPage(
                            Mapper.Map<ActivitiesPage.Item[]>(s.Results),
                            s.TotalResults
                        )
                    )
                ;

                CreateMap<Activity, ActivitiesPage.Item>()
                    .ForMember(d => d.Title, o => o.ResolveUsing(s =>
                        s.Values.Title ??
                        s.DraftedValues.Title ??
                        "New Activity #{0}".FormatWith(s.Number)
                    ))
                ;

                CreateMap<ActivityTag, ActivitiesPage.Item.Tag>()
                ;
            }
        }
    }
}