//using System.Web.Routing;
//using AutoMapper;
//using UCosmic.Domain.Languages;
//using System.ComponentModel.DataAnnotations;
//using System.Globalization;
//using System.Web.Mvc;

//namespace UCosmic.Www.Mvc.Areas.Languages.Models
//{
//    public class LanguageTableRow
//    {
//        public string TranslatedNameText { get; set; }
//        public string TwoLetterIsoCode { get; set; }
//        public string ThreeLetterIsoCode { get; set; }
//        public string Iso639Codes { get; set; }

//        [DisplayFormat(NullDisplayText = "-")]
//        public string NativeNameText { get; set; }

//        public int NamesCount { get; set; }

//        public bool IsUserLanguage { get; set; }
//    }

//    public static class LanguageTableRowProfiler
//    {
//        public class EntityToModelProfile : Profile
//        {
//            protected override void Configure()
//            {
//                CreateMap<Language, LanguageTableRow>()

//                    // do not display native name if if this the same as the translated name
//                    .ForMember(d => d.NativeNameText, o => o.ResolveUsing(s =>
//                        s.NativeName.Text == s.TranslatedName.Text ? null : s.NativeName.Text))

//                    // only one of the languages can be the user's default
//                    .ForMember(d => d.IsUserLanguage, o => o.ResolveUsing(s =>
//                        s.TwoLetterIsoCode.Equals(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)))

//                    // combine codes into a single column
//                    .ForMember(d => d.Iso639Codes, o => o.ResolveUsing(s =>
//                        {
//                            var combined = string.Format("{0}, {1}", s.TwoLetterIsoCode, s.ThreeLetterIsoCode);
//                            if (s.ThreeLetterIsoCode != s.ThreeLetterIsoBibliographicCode)
//                                combined = string.Format("{0}, {1}*", combined, s.ThreeLetterIsoBibliographicCode);
//                            return combined;
//                        }))
//                ;
//            }
//        }
//    }
//}