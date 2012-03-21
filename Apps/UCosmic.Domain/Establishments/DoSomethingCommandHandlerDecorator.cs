using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UCosmic.Domain.Establishments
{
    public class DoSomethingCommandHandlerDecorator<TCommand> : IHandleCommands<TCommand>
    {
        private readonly IHandleCommands<TCommand> _decorated;

        public DoSomethingCommandHandlerDecorator(IHandleCommands<TCommand> decorated)
        {
            _decorated = decorated;
        }

        public void Handle(TCommand command)
        {
            // do something...

            _decorated.Handle(command);

            // ...do something else
        }
    }
}
