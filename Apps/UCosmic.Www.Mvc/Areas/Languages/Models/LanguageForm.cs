using System.Linq;
using AutoMapper;
using UCosmic.Domain.Languages;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class LanguageForm
    {
        public string TranslatedNameText { get; set; }
        public string NativeNameText { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }

        public LanguageNameTable Names { get; set; }
    }

    public static class LanguageFormProfiler
    {
        public class LanguageFormProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Language, LanguageForm>()

                    // do not display native name if if this the same as the translated name
                    .ForMember(d => d.NativeNameText, o => o.ResolveUsing(s =>
                        s.NativeName.Text == s.TranslatedName.Text ? null : s.NativeName.Text))

                    .ForMember(d => d.Names, o => o.ResolveUsing(s =>
                    {
                        var names = Mapper.Map<LanguageNameTable>(s.Names)
                            .OrderByDescending(r => r.IsUserTranslation)
                            .ThenByDescending(r => r.IsNativeTranslation)
                            .ThenBy(r => r.TranslationToLanguageTranslatedNameText)
                        ;
                        return names;
                    }))
                ;
            }
        }
    }
}