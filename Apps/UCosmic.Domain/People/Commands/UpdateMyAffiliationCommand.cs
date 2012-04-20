using System.Security.Principal;
namespace UCosmic.Domain.People
{
    public class UpdateMyAffiliationCommand
    {
        public IPrincipal Principal { get; set; }
        public int EstablishmentId { get; set; }
        public string JobTitles { get; set; }
        public bool IsClaimingStudent { get; set; }
        public bool IsClaimingEmployee { get; set; }
        public bool IsClaimingInternationalOffice { get; set; }
        public bool IsClaimingAdministrator { get; set; }
        public bool IsClaimingFaculty { get; set; }
        public bool IsClaimingStaff { get; set; }
        public int ChangeCount { get; internal set; }
        public bool ChangedState { get { return ChangeCount > 0; } }
    }
}
