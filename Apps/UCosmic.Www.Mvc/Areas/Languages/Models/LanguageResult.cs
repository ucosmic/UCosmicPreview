using System.Globalization;
using AutoMapper;
using UCosmic.Domain.Languages;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class LanguageResult
    {
        public string TranslatedNameText { get; set; }
        public string NativeNameText { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
        public string ThreeLetterIsoBibliographicCode { get; set; }

        public int NamesCount { get; set; }

        public bool IsUserLanguage { get; set; }
    }

    public static class LanguageTableRowProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Language, LanguageResult>()

                    // only one of the languages can be the user's default
                    .ForMember(d => d.IsUserLanguage, o => o.ResolveUsing(s =>
                        s.TwoLetterIsoCode.Equals(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)))
                ;
            }
        }
    }
}