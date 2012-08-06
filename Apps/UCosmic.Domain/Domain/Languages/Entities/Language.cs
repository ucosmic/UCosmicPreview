using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace UCosmic.Domain.Languages
{
    public class Language : RevisableEntity
    {
        protected internal Language()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Names = new Collection<LanguageName>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public string TwoLetterIsoCode { get; protected internal set; }
        public string ThreeLetterIsoCode { get; protected internal set; }
        public string ThreeLetterIsoBibliographicCode { get; protected internal set; }

        public virtual ICollection<LanguageName> Names { get; protected set; }

        public LanguageName TranslateNameTo(string languageIsoCode)
        {
            if (string.IsNullOrWhiteSpace(languageIsoCode)) return null;

            return Names.SingleOrDefault(languageName =>
                languageName.TranslationToLanguage.TwoLetterIsoCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase) ||
                languageName.TranslationToLanguage.ThreeLetterIsoCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase) ||
                languageName.TranslationToLanguage.ThreeLetterIsoBibliographicCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase));
        }

        public LanguageName NativeName
        {
            get
            {
                if (Names.Count() == 1)
                    return Names.Single();

                var nativeName = Names.SingleOrDefault(languageName =>
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