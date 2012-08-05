//using System.Security.Principal;
//using ServiceLocatorPattern;
//using UCosmic.Domain.Identity;

//namespace UCosmic.Impl.Seeders
//{
//    public abstract class BaseRoleSeeder : UCosmicDbSeeder
//    {
//        //protected void EnsureRole0(string roleName, string roleDescription)
//        //{
//        //    var facade = ServiceProviderLocator.Current.GetService<RoleFacade>();
//        //    var role = facade.CreateOrUpdate(roleName, roleDescription);
//        //    Context.Entry(role).State = role.RevisionId == 0 ? EntityState.Added : EntityState.Modified;
//        //}

//        protected void EnsureRole(string roleName, string roleDescription)
//        {
//            var queryProcessor = ServiceProviderLocator.Current.GetService<IProcessQueries>();
//            var role = queryProcessor.Execute(new GetRoleBySlugQuery(roleName.Replace(" ", "-")));
//            if (role == null)
//            {
//                var createHandler = ServiceProviderLocator.Current.GetService<IHandleCommands<CreateRoleCommand>>();
//                createHandler.Handle(new CreateRoleCommand(roleName) { Description = roleDescription, });
//            }
//            else
//            {
//                var updateHandler = ServiceProviderLocator.Current.GetService<IHandleCommands<UpdateRoleCommand>>();
//                IIdentity identity = new GenericIdentity(GetType().Name);
//                IPrincipal principal = new GenericPrincipal(identity, new[] { RoleName.AuthorizationAgent });
//                updateHandler.Handle(new UpdateRoleCommand(principal)
//                {
//                    EntityId = role.EntityId,
//                    Description = roleDescription,
//                });
//            }
//        }
//    }
//}