using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public static class UpdateEmailValueProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(UpdateEmailValueProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EmailAddress, UpdateEmailValueForm>()
                    .ForMember(d => d.OldSpelling, opt => opt.MapFrom(s => s.Value))
                    .ForMember(d => d.ReturnUrl, opt => opt.Ignore())
                ;
            }
        }

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<UpdateEmailValueForm, UpdateMyEmailValueCommand>()
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.NewValue, o => o.MapFrom(s => s.Value))
                    .ForMember(d => d.ChangedState, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}