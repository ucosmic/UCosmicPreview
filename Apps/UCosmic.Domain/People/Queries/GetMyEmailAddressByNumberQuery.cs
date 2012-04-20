using System.Security.Principal;

namespace UCosmic.Domain.People
{
    public class GetMyEmailAddressByNumberQuery : IDefineQuery<EmailAddress>
    {
        public IPrincipal Principal { get; set; }
        public int Number { get; set; }
    }
}
