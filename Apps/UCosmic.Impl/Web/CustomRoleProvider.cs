﻿using System;
using System.Linq;
using System.Web.Security;
using UCosmic.Domain.Identity;
using UCosmic.Impl.Orm;

namespace UCosmic.Impl
{
    public class CustomRoleProvider : RoleProvider
    {
        private static UCosmicContext GetEntities()
        {
            // new up context to avoid ArgumentExceptions on Set<Role>().AsNoTracking()
            return new UCosmicContext();
        }

        public override string[] GetRolesForUser(string userName)
        {
            using (var entities = GetEntities())
            {
                // create a new handler
                var handler = new FindRolesGrantedToUserNameHandler(entities);

                // find roles granted to this user
                var roles = handler.Handle(
                    new FindRolesGrantedToUserNameQuery {UserName = userName}
                );

                // return the role names
                var roleNames = roles.Select(role => role.Name)
                    .Distinct()
                    .ToArray();
                return roleNames;
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