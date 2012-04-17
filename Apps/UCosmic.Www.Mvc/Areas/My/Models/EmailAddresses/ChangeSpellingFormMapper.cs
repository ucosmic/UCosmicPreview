using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models.EmailAddresses
{
    public static class ChangeSpellingFormMapper
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ChangeSpellingFormMapper));
        }

        // ReSharper disable UnusedMember.Local

        private class EntityToViewModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EmailAddress, ChangeSpellingForm>()
                    .ForMember(d => d.OldSpelling, opt => opt.MapFrom(s => s.Value))
                    .ForMember(d => d.ReturnUrl, opt => opt.Ignore())
                ;
            }
        }

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<ChangeSpellingForm, ChangeEmailAddressSpellingCommand>()
                    .ForMember(d => d.UserName, o => o.MapFrom(s => s.PersonUserName))
                    .ForMember(d => d.NewValue, o => o.MapFrom(s => s.Value))
                    .ForMember(d => d.ChangedState, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}