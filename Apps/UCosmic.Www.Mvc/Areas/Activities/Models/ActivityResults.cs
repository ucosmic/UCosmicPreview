using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using UCosmic.Domain.Activities;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Activities.Models
{
    public class ActivityResults : PageOf<ActivityResults.ActivityResult>
    {
        public ActivityResults(IEnumerable<ActivityResult> items, int totalResults)
            : base(items, totalResults) { }

        [Display(Prompt = "Type a country, institution, or topic")]
        public string Keyword { get; set; }

        [ScaffoldColumn(false)]
        public string Establishment { get; set; }

        [ScaffoldColumn(false)]
        public string TenantName { get; set; }

        public class ActivityResult
        {
            public Guid EntityId { get; set; }

            public string Title { get; set; }

            [DisplayFormat(DataFormatString = "{0:M/d/yyyy}")]
            public DateTime StartsOn { get; set; }

            public Owner Person { get; set; }
            public class Owner
            {
                public string DisplayName { get; set; }
            }
        }
    }

    public static class ActivityResultsProfiler
    {
        public class EntitiesToModelsProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<PagedResult<Activity>, ActivityResults>()
                    .ConstructUsing(s =>
                        new ActivityResults(
                            Mapper.Map<ActivityResults.ActivityResult[]>(s.Results),
                            s.TotalResults
                        )
                    )
                    .ForMember(d => d.Items, o => o.Ignore())
                    .ForMember(d => d.Keyword, o => o.Ignore())
                    .ForMember(d => d.Establishment, o => o.Ignore())
                    .ForMember(d => d.TenantName, o => o.Ignore())
                ;

                CreateMap<Activity, ActivityResults.ActivityResult>()
                    .ForMember(d => d.Title, o => o.MapFrom(s => s.Values.Title))
                    .ForMember(d => d.StartsOn, o => o.MapFrom(s => s.Values.StartsOn))
                ;

                CreateMap<Person, ActivityResults.ActivityResult.Owner>()
                ;
            }
        }

        public class EstablishmentToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Establishment, ActivityResults>()
                    .ForMember(d => d.TenantName, o => o.MapFrom(s => s.OfficialName))
                    .ForMember(d => d.Keyword, o => o.Ignore())
                    .ForMember(d => d.Establishment, o => o.Ignore())
                    .ForMember(d => d.Items, o => o.Ignore())
                    .ForMember(d => d.TotalResults, o => o.Ignore())
                ;
            }
        }
    }
}