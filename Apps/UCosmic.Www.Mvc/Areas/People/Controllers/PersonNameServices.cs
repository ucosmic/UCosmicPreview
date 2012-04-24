namespace UCosmic.Www.Mvc.Areas.People.Controllers
{
    public class PersonNameServices
    {
        public PersonNameServices(
            IProcessQueries queryProcessor
        )
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }
}