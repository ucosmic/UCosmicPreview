using UCosmic.Domain;
using UCosmic.Domain.Identity;

namespace UCosmic.Www.Mvc.Areas.Identity.Services
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