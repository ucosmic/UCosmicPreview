using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public class AffiliationServices
    {
        public AffiliationServices(
            IProcessQueries queryProcessor
            //, IHandleCommands<ChangeEmailSpellingCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            //CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        //public IHandleCommands<ChangeEmailSpellingCommand> CommandHandler { get; private set; }
    }
}