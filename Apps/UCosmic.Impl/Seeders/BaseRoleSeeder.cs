using System.Data;
using UCosmic.Domain.Identity;

namespace UCosmic.Impl.Seeders
{
    public abstract class BaseRoleSeeder : UCosmicDbSeeder
    {
        protected void EnsureRole(string roleName, string roleDescription)
        {
            var facade = DependencyInjector.Current.GetService<RoleFacade>();
            var role = facade.CreateOrUpdate(roleName, roleDescription);
            Context.Entry(role).State = role.RevisionId == 0 ? EntityState.Added : EntityState.Modified;
        }
    }
}