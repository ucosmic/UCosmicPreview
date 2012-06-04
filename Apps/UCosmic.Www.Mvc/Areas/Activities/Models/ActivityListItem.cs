using AutoMapper;
using UCosmic.Domain.Activities;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Activities.Models
{
    public class ActivityListItem
    {
        public int Total { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public ActivityMode Mode { get; set; }
    }

    public static class ListItemProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(ListItemProfiler));
        }

        private class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Activity, ActivityListItem>()
                    .ForMember(d => d.Title, o => o.ResolveUsing(s => 
                        s.Values.Title ?? 
                        s.DraftedValues.Title ?? 
                        "New Activity #{0}".FormatWith(s.Number)
                    ))
                ;
            }
        }
    }
}