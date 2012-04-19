using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.People.Models
{
    public static class GenerateDisplayNameProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(GenerateDisplayNameProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class ViewModelToQueryProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<GenerateDisplayNameForm, GenerateDisplayNameQuery>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}