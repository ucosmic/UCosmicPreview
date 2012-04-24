using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public class UpdateNameServices
    {
        public UpdateNameServices(
            IProcessQueries queryProcessor
            , IHandleCommands<UpdateMyNameCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<UpdateMyNameCommand> CommandHandler { get; private set; }
    }
}