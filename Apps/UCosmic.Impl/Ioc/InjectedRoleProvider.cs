//using System;
//using System.Web.Security;

//namespace UCosmic.Impl
//{
//    public class InjectedRoleProvider : RoleProvider
//    {
//        private static IServiceProvider Injector { get { return DependencyInjector.Current; } }

//        private static RoleProvider Provider { get { return Injector.GetService<RoleProvider>(); } }

//        private static T WithProvider<T>(Func<RoleProvider, T> f)
//        {
//            return f(Provider);
//        }

//        private static void WithProvider(Action<RoleProvider> f)
//        {
//            f(Provider);
//        }

//        public override bool IsUserInRole(string username, string roleName)
//        {
//            return WithProvider(p => p.IsUserInRole(username, roleName));
//        }

//        public override string[] GetRolesForUser(string username)
//        {
//            return WithProvider(p => p.GetRolesForUser(username));
//        }

//        public override void CreateRole(string roleName)
//        {
//            WithProvider(p => p.CreateRole(roleName));
//        }

//        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
//        {
//            return WithProvider(p => p.DeleteRole(roleName, throwOnPopulatedRole));
//        }

//        public override bool RoleExists(string roleName)
//        {
//            return WithProvider(p => p.RoleExists(roleName));
//        }

//        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
//        {
//            WithProvider(p => p.AddUsersToRoles(usernames, roleNames));
//        }

//        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
//        {
//            WithProvider(p => p.RemoveUsersFromRoles(usernames, roleNames));
//        }

//        public override string[] GetUsersInRole(string roleName)
//        {
//            return WithProvider(p => p.GetUsersInRole(roleName));
//        }

//        public override string[] GetAllRoles()
//        {
//            return WithProvider(p => p.GetAllRoles());
//        }

//        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
//        {
//            return WithProvider(p => p.FindUsersInRole(roleName, usernameToMatch));
//        }

//        public override string ApplicationName
//        {
//            get { return WithProvider(p => p.ApplicationName); }
//            set { WithProvider(p => p.ApplicationName = value); }
//        }
//    }
//}