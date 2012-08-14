using AutoMapper;
using UCosmic.Domain.Identity;

namespace UCosmic.Www.Mvc.Areas.Preferences.Models
{
    public class MyPreference
    {
        public PreferenceCategory Category { get; set; }
        public PreferenceKey Key { get; set; }
        public string Value { get; set; }
    }

    public static class MyPreferenceProfiler
    {
        public class ModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<MyPreference, UpdateMyPreference>();
            }
        }
    }
}