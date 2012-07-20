using System;
using System.Web.Security;

namespace UCosmic.Impl
{
    public class DotNetMembershipProvider : IStorePasswords
    {
        public int MaximumPasswordAttempts
        {
            get { return Membership.MaxInvalidPasswordAttempts; }
        }

        public int MinimumPasswordLength
        {
            get { return Membership.MinRequiredPasswordLength; }
        }

        public bool Exists(string userName)
        {
            var member = Membership.GetUser(userName);
            return (member != null);
        }

        public bool Validate(string userName, string password)
        {
            return Membership.ValidateUser(userName, password);
        }

        public bool IsLockedOut(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user == null)
                throw new InvalidOperationException(string.Format(
                    "Cannot get lockout status because user '{0}' does not exist.",
                        userName));
            return user.IsLockedOut;
        }

        public void Create(string userName, string password)
        {
            Membership.CreateUser(userName, password, userName);
        }

        public void Destroy(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user == null)
                throw new InvalidOperationException(string.Format(
                    "Cannot revoke because user '{0}' does not exist.",
                        userName));
            Membership.DeleteUser(userName, true);
        }

        public void Reset(string userName, string password)
        {
            var user = Membership.GetUser(userName);
            if (user == null)
                throw new InvalidOperationException(string.Format(
                    "Cannot reset password because user '{0}' does not exist.",
                        userName));

            // unlock the user
            if (user.IsLockedOut) user.UnlockUser();

            if (!user.ChangePassword(user.ResetPassword(), password))
                throw new InvalidOperationException(string.Format(
                    "Password reset failed for user '{0}'.",
                        userName));
        }

        public void Update(string userName, string oldPassword, string newPassword)
        {
            var user = Membership.GetUser(userName);
            if (user == null)
                throw new InvalidOperationException(string.Format(
                    "Cannot change password because user '{0}' does not exist.",
                        userName));

            if (!user.ChangePassword(oldPassword, newPassword))
                throw new InvalidOperationException(string.Format(
                    "Password change failed for user '{0}'.",
                        userName));
        }
    }
}