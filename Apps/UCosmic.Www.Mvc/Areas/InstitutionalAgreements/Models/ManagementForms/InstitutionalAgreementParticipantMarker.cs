using System;
using System.Collections.Generic;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementParticipantMarker
    {
        public string ForEstablishmentOfficialName { get; set; }
        public string ForEstablishmentTranslatedName { get; set; }
        public string EstablishmentTranslatedThenOfficialName
        {
            get
            {
                var name = ForEstablishmentOfficialName;
                if (!ForEstablishmentOfficialName.Equals(ForEstablishmentTranslatedName))
                {
                    name = string.Format("{0} ({1})", ForEstablishmentTranslatedName, ForEstablishmentOfficialName);
                }
                return name;
            }
        }
        public Guid ForEstablishmentEntityId { get; set; }
        public double? CenterLatitude { get; set; }
        public double? CenterLongitude { get; set; }
        public ICollection<InstitutionalAgreementMapSearchResult> Agreements { get; set; }
    }
}