using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.My.Models.EmailAddresses
{
    public static class ChangeSpellingFormMapper
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ChangeSpellingFormMapper));
        }

        private class ChangeSpellingFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<EmailAddress, ChangeSpellingForm>()
                    .ForMember(target => target.OldSpelling, opt => opt.MapFrom(source => source.Value))
                    .ForMember(target => target.ReturnUrl, opt => opt.Ignore())
                ;
            }
        }
    }
}