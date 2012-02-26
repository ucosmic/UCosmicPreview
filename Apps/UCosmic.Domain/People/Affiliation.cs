using System.ComponentModel.DataAnnotations;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.People
{
    public class Affiliation : RevisableEntity
    {
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        public int EstablishmentId { get; set; }
        public virtual Establishment Establishment { get; set; }

        [StringLength(500)]
        public string JobTitles { get; set; }

        public bool IsDefault { get; set; }

        public bool IsAcknowledged { get; set; }
        public bool IsClaimingStudent { get; set; }
        public bool IsClaimingEmployee { get; set; }

        public bool IsClaimingInternationalOffice { get; set; }
        public bool IsClaimingAdministrator { get; set; }
        public bool IsClaimingFaculty { get; set; }
        public bool IsClaimingStaff { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Person.DisplayName, Establishment.OfficialName);
        }
    }
}