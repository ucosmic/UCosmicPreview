using UCosmic.Domain;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public class UpdateNameServices
    {
        public UpdateNameServices(
            IProcessQueries queryProcessor
            , IHandleCommands<UpdateNameCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<UpdateNameCommand> CommandHandler { get; private set; }
    }
}