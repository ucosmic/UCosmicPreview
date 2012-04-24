using AutoMapper;
using UCosmic.Domain.Email;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Passwords.Models
{
    public static class ForgotPasswordProfiler
    {
        public static void RegisterProfiles()
        {
            DefaultModelMapper.RegisterProfiles(typeof(ForgotPasswordProfiler));
        }

        // ReSharper disable UnusedMember.Local

        private class ViewModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<ForgotPasswordForm, SendEmailConfirmationMessageCommand>()
                    .ForMember(d => d.Intent, o => o.UseValue(EmailConfirmationIntent.PasswordReset))
                    .ForMember(d => d.ConfirmationToken, o => o.Ignore())
                ;
            }
        }

        // ReSharper restore UnusedMember.Local
    }
}