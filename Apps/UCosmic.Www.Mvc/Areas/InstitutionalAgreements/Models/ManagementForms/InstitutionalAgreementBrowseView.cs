using System.Collections.Generic;
using System.Linq;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementBrowseView
    {
        public ICollection<InstitutionalAgreementParticipantMarker> ParticipantMarkers { get; set; }
        public ICollection<InstitutionalAgreementParticipantMarker> UnlocatedParticipants
        {
            get { return ParticipantMarkers.Where(m => !m.CenterLatitude.HasValue || !m.CenterLongitude.HasValue).ToList(); }
        }
        public ICollection<InstitutionalAgreementSearchResult> TableResults { get; set; }
        public int CountryCount { get; set; }
    }
}