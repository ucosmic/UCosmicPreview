using AutoMapper;
using UCosmic.Domain.Activities;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Activities.Models
{
    public class ActivitiesPage : PageOf<ActivitiesPage.Item>
    {
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
        public class EntitiesToModelsProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<PagedResult<Activity>, ActivitiesPage>();

                CreateMap<Activity, ActivitiesPage.Item>()
                    .ForMember(d => d.Title, o => o.ResolveUsing(s =>
                        s.Values.Title ??
                        s.DraftedValues.Title ??
                        "New Activity #{0}".FormatWith(s.Number)
                    ))
                ;

                CreateMap<ActivityTag, ActivitiesPage.Item.Tag>();
            }
        }
    }
}