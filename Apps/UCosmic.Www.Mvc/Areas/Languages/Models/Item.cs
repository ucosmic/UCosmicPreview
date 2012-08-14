using System.Linq;
using AutoMapper;
using UCosmic.Domain.Languages;

namespace UCosmic.Www.Mvc.Areas.Languages.Models
{
    public class Item
    {
        public string TranslatedNameText { get; set; }
        public string NativeNameText { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }

        public ItemNames Names { get; set; }
    }

    public static class ItemProfiler
    {
        public class EntityToModelProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<Language, Item>()

                    // do not display native name if if this the same as the translated name
                    .ForMember(d => d.NativeNameText, o => o.ResolveUsing(s =>
                        s.NativeName.Text == s.TranslatedName.Text ? null : s.NativeName.Text))

                    .ForMember(d => d.Names, o => o.ResolveUsing(s =>
                    {
                        var names = Mapper.Map<ItemNames>(s.Names)
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