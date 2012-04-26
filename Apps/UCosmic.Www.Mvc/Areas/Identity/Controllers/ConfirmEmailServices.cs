namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    public class ConfirmEmailServices
    {
        public ConfirmEmailServices(IProcessQueries queryProcessor)
        {
            QueryProcessor = queryProcessor;
        }

        public IProcessQueries QueryProcessor { get; private set; }
    }
}