using System.Web.Security;

namespace UCosmic
{
    public class DotNetMembershipProvider : ISignMembers
    {
        public bool IsSignedUp(string userName)
        {
            var member = Membership.GetUser(userName);
            return (member != null);
        }

        public void SignUp(string userName, string password)
        {
            Membership.CreateUser(userName, password, userName);
        }
    }
}