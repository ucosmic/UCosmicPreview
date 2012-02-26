using System.Web.Security;

namespace UCosmic
{
    public class DotNetMembershipSigner : ISignMembers
    {
        public bool IsSignedUp(string userName)
        {
            var member = Membership.GetUser(userName);
            return (member != null);
        }

        public void SignIn(string userName)
        {
            FormsAuthentication.SetAuthCookie(userName, false);
        }

        public void SignUp(string userName, string password)
        {
            Membership.CreateUser(userName, password, userName);
        }

        public string DefaultSignedInUrl
        {
            get { return FormsAuthentication.DefaultUrl; }
        }
    }

}