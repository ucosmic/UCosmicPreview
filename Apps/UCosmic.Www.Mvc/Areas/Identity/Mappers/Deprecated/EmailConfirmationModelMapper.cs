using AutoMapper;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Areas.Identity.Models.Deprecated;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
{
    public static class EmailConfirmationModelMapper
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(EmailConfirmationModelMapper));
        }

        // ReSharper disable UnusedMember.Local

        //private class ConfirmEmailFormProfile : Profile
        //{
        //    protected override void Configure()
        //    {
        //        // NOTE: must ignore secret code, it should not be passed into the form.
        //        CreateMap<EmailConfirmation, ConfirmEmailForm>()
        //            .ForMember(dto => dto.SecretCode, opt => opt.Ignore())
        //        ;
        //    }
        //}

        private class ConfirmEmailForgotPasswordFormProfile : Profile
        {
            protected override void Configure()
            {
                // NOTE: must ignore secret code, it should not be passed into the form.
                CreateMap<EmailConfirmation, ConfirmEmailForgotPasswordForm>()
                    .ForMember(dto => dto.SecretCode, opt => opt.Ignore());
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}