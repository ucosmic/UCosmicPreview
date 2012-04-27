using System;
using System.Web.Security;

namespace UCosmic.Impl
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

        public void ResetPassword(string userName, string password)
        {
            var user = Membership.GetUser(userName);
            if (user == null)
                throw new InvalidOperationException(string.Format(
                    "Cannot reset password because user '{0}' does not exist",
                        userName));

            if (!user.ChangePassword(user.ResetPassword(), password))
                throw new InvalidOperationException(string.Format(
                    "Password reset failed for user '{0}'.",
                        userName));
        }
    }
}