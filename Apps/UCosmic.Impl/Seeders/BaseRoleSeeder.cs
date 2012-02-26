using UCosmic.Domain.Identity;
using UCosmic.Orm;

namespace UCosmic.Seeders
{
    public abstract class BaseRoleSeeder : UCosmicDbSeeder
    {
        protected void EnsureRole(string roleName, string roleDescription)
        {
            var roleFinder = new RoleFinder(Context);
            var objectCommander = new ObjectCommander(Context);
            var role = roleFinder.FindOne(RoleBy.Name(roleName).ForInsertOrUpdate());
            if (role != null) return;
            role = new Role(roleName)
            {
                Description = roleDescription,
            };
            objectCommander.Insert(role, true);
        }
    }
}