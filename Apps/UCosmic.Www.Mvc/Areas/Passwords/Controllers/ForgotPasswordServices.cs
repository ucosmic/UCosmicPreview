using UCosmic.Domain.Email;

namespace UCosmic.Www.Mvc.Areas.Passwords.Controllers
{
    public class ForgotPasswordServices
    {
        public ForgotPasswordServices(
            IProcessQueries queryProcessor
        , IHandleCommands<SendConfirmEmailMessageCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<SendConfirmEmailMessageCommand> CommandHandler { get; private set; }
    }
}