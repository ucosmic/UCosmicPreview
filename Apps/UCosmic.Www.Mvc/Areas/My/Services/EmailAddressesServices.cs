using UCosmic.Domain;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Services
{
    public class EmailAddressesServices
    {
        public EmailAddressesServices(
            IProcessQueries queryProcessor
            , IHandleCommands<ChangeEmailAddressSpellingCommand> changeSpellingHandler
        )
        {
            QueryProcessor = queryProcessor;
            ChangeEmailAddressSpellingHandler = changeSpellingHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        public IHandleCommands<ChangeEmailAddressSpellingCommand> ChangeEmailAddressSpellingHandler { get; private set; }
    }
}