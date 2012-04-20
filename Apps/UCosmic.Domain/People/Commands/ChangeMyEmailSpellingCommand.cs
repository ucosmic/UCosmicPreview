using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class ChangeMyEmailSpellingCommand
    {
        public IPrincipal Principal { get; set; }
        public int Number { get; set; }
        public string NewValue { get; set; }
        public bool ChangedState { get; internal set; }
    }
}
