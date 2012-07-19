using System;
using System.Linq;
using System.Web.Security;
using UCosmic.Domain.Identity;
using UCosmic.Impl.Orm;

namespace UCosmic.Impl
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly IQueryEntities _entities;

        public CustomRoleProvider()
        {
            // asp.net role provider behaves as a singleton, so we can't inject the PerWebRequest
            // instances of DbContext, because those are disposed of at the end of a web request.
            // instead take a hard dependency on the DbContext to hold an undisposed instance.
            _entities = new UCosmicContext(null);
        }

        public override string[] GetRolesForUser(string userName)
        {
            // create a new handler
            var handler = new FindRolesGrantedToUserNameHandler(_entities);

            // find roles granted to this user
            var roles = handler.Handle(
                new FindRolesGrantedToUserNameQuery { UserName = userName }
            );

            // return the role names
            var roleNames = roles.Select(role => role.Name)
                .Distinct()
                .ToArray();
            return roleNames;
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