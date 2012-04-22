using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    public class PersonInfoServices
    {
        public PersonInfoServices(
            IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }
}