using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public class ProfileServices
    {
        public ProfileServices(
            IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }
}