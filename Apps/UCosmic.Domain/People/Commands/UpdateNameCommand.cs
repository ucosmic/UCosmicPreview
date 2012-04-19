using System.Security.Principal;
namespace UCosmic.Domain.People
{
    public class UpdateNameCommand
    {
        public IPrincipal Principal { get; set; }
        public bool IsDisplayNameDerived { get; set; }
        public string DisplayName { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int ChangeCount { get; internal set; }
        public bool ChangedState { get { return ChangeCount > 0; } }
    }
}
