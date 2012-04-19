using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public static class UpdateNameProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(UpdateNameProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Person, UpdateNameForm>();
            }
        }

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<UpdateNameForm, UpdateNameCommand>()
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.ChangeCount, o => o.Ignore())
                ;
            }
        }

        private class ViewModelToGenerateDisplayNameQueryProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<UpdateNameForm, GenerateDisplayNameQuery>();
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}