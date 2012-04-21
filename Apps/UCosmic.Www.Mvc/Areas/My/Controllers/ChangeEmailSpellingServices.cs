using UCosmic.Domain;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Controllers
{
    public class ChangeEmailSpellingServices
    {
        public ChangeEmailSpellingServices(
            IProcessQueries queryProcessor
            , IHandleCommands<UpdateMyEmailValueCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<UpdateMyEmailValueCommand> CommandHandler { get; private set; }
    }
}