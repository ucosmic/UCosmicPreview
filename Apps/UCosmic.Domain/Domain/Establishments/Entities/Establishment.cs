using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Establishments
{
    public class Establishment : RevisableEntity
    {
        protected internal Establishment()
        {
            PublicContactInfo = new EstablishmentContactInfo();
            PartnerContactInfo = new EstablishmentContactInfo();

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            EmailDomains = new Collection<EstablishmentEmailDomain>();
            Urls = new Collection<EstablishmentUrl>();
            Names = new Collection<EstablishmentName>();
            Affiliates = new Collection<Affiliation>();
            Ancestors = new Collection<EstablishmentNode>();
            Children = new Collection<Establishment>();
            Offspring = new Collection<EstablishmentNode>();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public string OfficialName { get; protected internal set; }
        public virtual ICollection<EstablishmentName> Names { get; protected internal set; }

        public EstablishmentName TranslateNameTo(string languageIsoCode)
        {
            if (string.IsNullOrWhiteSpace(languageIsoCode)) return null;

            return Names.FirstOrDefault(establishmentName => establishmentName.TranslationToLanguage != null && !establishmentName.IsFormerName && (
                establishmentName.TranslationToLanguage.TwoLetterIsoCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase) ||
                establishmentName.TranslationToLanguage.ThreeLetterIsoCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase) ||
                establishmentName.TranslationToLanguage.ThreeLetterIsoBibliographicCode.Equals(languageIsoCode, StringComparison.OrdinalIgnoreCase)));
        }

        public EstablishmentName TranslatedName
        {
            get
            {
                var currentUiName = TranslateNameTo(
                    CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
                return currentUiName ?? TranslateNameTo("en") ?? Names.Single(n => n.IsOfficialName);
            }
        }

        public string WebsiteUrl { get; protected internal set; }
        public virtual ICollection<EstablishmentUrl> Urls { get; protected set; }

        public virtual Establishment Parent { get; protected internal set; }
        public virtual ICollection<EstablishmentNode> Ancestors { get; protected internal set; }
        public virtual ICollection<Establishment> Children { get; protected set; }
        public virtual ICollection<EstablishmentNode> Offspring { get; protected set; }

        public virtual EstablishmentLocation Location { get; protected internal set; }
        public virtual EstablishmentSamlSignOn SamlSignOn { get; protected internal set; }
        public bool HasSamlSignOn() { return SamlSignOn != null && IsMember; }

        public bool IsMember { get; protected internal set; }
        public bool IsAncestorMember
        {
            get
            {
                var currentParent = Parent;
                while (currentParent != null)
                {
                    if (currentParent.IsMember)
                    {
                        return true;
                    }
                    currentParent = currentParent.Parent;
                }
                return false;
            }
        }

        public virtual ICollection<EstablishmentEmailDomain> EmailDomains { get; protected internal set; }
        public virtual ICollection<Affiliation> Affiliates { get; protected internal set; }

        public virtual EstablishmentType Type { get; protected internal set; }
        public bool IsInstitution
        {
            get { return Type.Category.Code == EstablishmentCategoryCode.Inst; }
        }

        //public InstitutionInfo InstitutionInfo { get; protected internal set; }
        public string CollegeBoardDesignatedIndicator { get; protected internal set; }
        public string UCosmicCode { get; protected internal set; }
        public EstablishmentContactInfo PublicContactInfo { get; protected internal set; }
        public EstablishmentContactInfo PartnerContactInfo { get; protected internal set; }

        public override string ToString()
        {
            return OfficialName;
        }
    }
}