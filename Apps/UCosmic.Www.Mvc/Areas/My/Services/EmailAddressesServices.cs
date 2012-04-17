using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Areas.My.Services
{
    public class EmailAddressesServices
    {
        public EmailAddressesServices(
            IProcessQueries queryProcessor
            //, IHandleCommands<SendSamlAuthnRequestCommand> authnRequestHandler
            //, IHandleCommands<SignOnSamlUserCommand> authnResponseHandler
        )
        {
            QueryProcessor = queryProcessor;
            //SendSamlAuthnRequestHandler = authnRequestHandler;
            //SignOnSamlUserHandler = authnResponseHandler;
        }

        public IProcessQueries QueryProcessor { get; private set; }
        //public IHandleCommands<SendSamlAuthnRequestCommand> SendSamlAuthnRequestHandler { get; private set; }
        //public IHandleCommands<SignOnSamlUserCommand> SignOnSamlUserHandler { get; private set; }

    }
}