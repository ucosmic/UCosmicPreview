using UCosmic.Domain.Identity;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class RolesServices
    {
        public RolesServices(RoleFacade roles
            , IProcessQueries queryProcessor
        )
        {
            Roles = roles;
            QueryProcessor = queryProcessor;
        }

        public RoleFacade Roles { get; private set; }
        public IProcessQueries QueryProcessor { get; private set; }
    }
}