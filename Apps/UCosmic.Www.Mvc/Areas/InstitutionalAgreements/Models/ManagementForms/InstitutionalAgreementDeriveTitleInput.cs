using System;
using System.Collections.Generic;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementDeriveTitleInput
    {
        public bool IsTitleDerived { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public List<Guid> ParticipantEstablishmentIds { get; set; }
    }
}