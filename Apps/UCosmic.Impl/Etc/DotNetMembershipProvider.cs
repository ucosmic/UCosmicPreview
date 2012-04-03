using System.Web.Security;

namespace UCosmic
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class DotNetMembershipProvider : ISignMembers
    // ReSharper restore ClassNeverInstantiated.Global
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