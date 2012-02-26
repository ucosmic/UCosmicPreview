using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace UCosmic.Domain.Languages
{
    public class Language : RevisableEntity
    {
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string TwoLetterIsoCode { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string ThreeLetterIsoCode { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string ThreeLetterIsoBibliographicCode { get; set; }

        public virtual ICollection<LanguageName> Names { get; set; }

        public LanguageName TranslateNameTo(string languageIsoCode)
        {
            if (string.IsNullOrWhiteSpace(languageIsoCode)) return null;

            return Names.Current().SingleOrDefault(languageName =>
                languageName.TranslationToLanguage.TwoLetterIsoCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase) ||
                languageName.TranslationToLanguage.ThreeLetterIsoCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase) ||
                languageName.TranslationToLanguage.ThreeLetterIsoBibliographicCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase));
        }

        public LanguageName NativeName
        {
            get
            {
                if (Names.Current().Count() == 1)
                    return Names.Current().Single();

                var nativeName = Names.Current().SingleOrDefault(languageName =>
                    languageName.TranslationToLanguage == this);

                return nativeName;
            }
        }

        public LanguageName TranslatedName
        {
            get
            {
                var currentUiName = TranslateNameTo(
                    CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
                return currentUiName ?? TranslateNameTo("en") ?? NativeName;
            }
        }

    }
}