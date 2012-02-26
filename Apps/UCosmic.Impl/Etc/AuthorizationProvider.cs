using System;
using System.Linq;
using System.Web.Security;
using UCosmic.Domain;
using UCosmic.Domain.Identity;
using UCosmic.Orm;

namespace UCosmic
{
    public class AuthorizationProvider : RoleProvider
    {
        private readonly RoleFinder _roles;

        public AuthorizationProvider()
        {
            _roles = new RoleFinder(new UCosmicContext());
        }

        public override string[] GetRolesForUser(string userName)
        {
            return TryGetRolesForUser(userName, 0);
        }

        private string[] TryGetRolesForUser(string userName, int retryCount)
        {
            try
            {
                var roles = _roles.FindMany(RolesWith.GrantToUser(userName))
                    .Current()
                    .Select(role => role.Name)
                    .Distinct().ToArray();
                return roles;
            }
            catch (Exception)
            {
                if (retryCount <= 3)
                    return TryGetRolesForUser(userName, ++retryCount);
                throw;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotSupportedException("Please use RoleProvider.GetRolesForUser instead.");
        }

        public override void CreateRole(string roleName)
        {
            throw new NotSupportedException("Please use RoleProvider.GetRolesForUser instead.");
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotSupportedException("Please use RoleProvider.GetRolesForUser instead.");
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotSupportedException("Please use RoleProvider.GetRolesForUser instead.");
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException("Please use RoleProvider.GetRolesForUser instead.");
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException("Please use RoleProvider.GetRolesForUser instead.");
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotSupportedException("Please use RoleProvider.GetRolesForUser instead.");
        }

        public override string[] GetAllRoles()
        {
            throw new NotSupportedException("Please use RoleProvider.GetRolesForUser instead.");
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotSupportedException("Please use RoleProvider.GetRolesForUser instead.");
        }

        public override string ApplicationName
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }
    }

}