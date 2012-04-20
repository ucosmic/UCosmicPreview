using UCosmic.Domain;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public class AffiliationServices
    {
        public AffiliationServices(
            IProcessQueries queryProcessor
            , IHandleCommands<UpdateMyAffiliationCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<UpdateMyAffiliationCommand> CommandHandler { get; private set; }
    }
}