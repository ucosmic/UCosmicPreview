//using AutoMapper;
//using UCosmic.Domain.People;
//using UCosmic.Www.Mvc.Areas.Identity.Models.Password;
//using UCosmic.Www.Mvc.Models;

//namespace UCosmic.Www.Mvc.Areas.Identity.Mappers
//{
//    public static class PasswordModelMapper
//    {
//        public static void RegisterProfiles()
//        {
//            RootModelProfiler.RegisterProfiles(typeof(PasswordModelMapper));
//        }

//        // ReSharper disable UnusedMember.Local

//        private class ResetPasswordFormProfile : Profile
//        {
//            protected override void Configure()
//            {
//                CreateMap<EmailConfirmation, ResetPasswordForm>()
//                    .ForMember(m => m.Password, opt => opt.Ignore())
//                    .ForMember(m => m.ConfirmPassword, opt => opt.Ignore())
//                ;
//            }
//        }

//        //private class CreatePasswordFormProfile : Profile
//        //{
//        //    protected override void Configure()
//        //    {
//        //        CreateMap<EmailConfirmation, CreatePasswordForm>()
//        //            .ForMember(m => m.Password, opt => opt.Ignore())
//        //            .ForMember(m => m.ConfirmPassword, opt => opt.Ignore())
//        //        ;
//        //    }
//        //}

//        // ReSharper restore UnusedMember.Local
//    }
//}