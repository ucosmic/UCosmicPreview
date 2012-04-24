using UCosmic.Domain.Email;

namespace UCosmic.Www.Mvc.Areas.Passwords.Controllers
{
    public class ForgotPasswordServices
    {
        public ForgotPasswordServices(
            IProcessQueries queryProcessor
        , IHandleCommands<SendPasswordResetMessageCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<SendPasswordResetMessageCommand> CommandHandler { get; private set; }
    }
}