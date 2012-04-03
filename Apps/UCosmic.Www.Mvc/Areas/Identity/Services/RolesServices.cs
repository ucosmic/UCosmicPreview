using UCosmic.Domain.Identity;

namespace UCosmic.Www.Mvc.Areas.Identity.Services
{
    public class RolesServices
    {
        public RolesServices(RoleFacade roles
            , UserFacade users
        )
        {
            Roles = roles;
            Users = users;
        }

        public RoleFacade Roles { get; private set; }
        public UserFacade Users { get; private set; }
    }
}