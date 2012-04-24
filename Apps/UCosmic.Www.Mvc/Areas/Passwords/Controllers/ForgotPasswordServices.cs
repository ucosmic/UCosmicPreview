using UCosmic.Domain.Email;

namespace UCosmic.Www.Mvc.Areas.Passwords.Controllers
{
    public class ForgotPasswordServices
    {
        public ForgotPasswordServices(
            IProcessQueries queryProcessor
        , IHandleCommands<SendEmailConfirmationMessageCommand> commandHandler
        )
        {
            QueryProcessor = queryProcessor;
            CommandHandler = commandHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<SendEmailConfirmationMessageCommand> CommandHandler { get; private set; }
    }
}