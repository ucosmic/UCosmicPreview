using System;
using System.Collections.Generic;
using System.Linq;
using UCosmic.Domain.Places;
using System.Globalization;

namespace UCosmic.Domain.Establishments
{
    public class EstablishmentLocation : RevisableEntity
    {
        public EstablishmentLocation()
        {
            Center = new Coordinates();
            BoundingBox = new BoundingBox();
        }

        public virtual Establishment ForEstablishment { get; set; }

        public virtual ICollection<EstablishmentAddress> Addresses { get; set; }

        public EstablishmentAddress TranslateAddressTo(string languageIsoCode)
        {
            if (string.IsNullOrWhiteSpace(languageIsoCode)) return null;

            return Addresses.Current().SingleOrDefault(a =>
                a.TranslationToLanguage.TwoLetterIsoCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase) ||
                a.TranslationToLanguage.ThreeLetterIsoCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase) ||
                a.TranslationToLanguage.ThreeLetterIsoBibliographicCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase));
        }

        public EstablishmentAddress DefaultAddress
        {
            get
            {
                if (Addresses.Current().Count() == 1)
                    return Addresses.Current().Single();

                var currentUiAddress = TranslateAddressTo(
                    CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

                return currentUiAddress ?? TranslateAddressTo("en");
            }
        }

        public Coordinates Center { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public int? GoogleMapZoomLevel { get; set; }

        public virtual ICollection<Place> Places { get; set; }
    }
}