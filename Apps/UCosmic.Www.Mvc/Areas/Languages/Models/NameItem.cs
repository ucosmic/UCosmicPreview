using System.Globalization;
using AutoMapper;
using UCosmic.Domain.Languages;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class NameItem
    {
        public string Text { get; set; }
        public bool IsUserTranslation { get; set; }
        public bool IsNativeTranslation { get; set; }
        public string TranslationToLanguageTranslatedNameText { get; set; }
    }

    public static class NameItemProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<LanguageName, NameItem>()

                    // only one of the languages can be the user's default
                    .ForMember(d => d.IsUserTranslation, o => o.ResolveUsing(s =>
                        s.TranslationToLanguage.TwoLetterIsoCode.Equals(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)))

                    // only one of the languages can be the native translation
                    .ForMember(d => d.IsNativeTranslation, o => o.ResolveUsing(s =>
                        s.TranslationToLanguage.TwoLetterIsoCode.Equals(s.Owner.TwoLetterIsoCode)))

                ;
            }
        }
    }
}