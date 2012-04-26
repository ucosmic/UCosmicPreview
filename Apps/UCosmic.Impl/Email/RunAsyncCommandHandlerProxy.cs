using System;
using System.Threading.Tasks;

namespace UCosmic
{
    public class RunAsyncCommandHandlerProxy<TCommand> : IHandleCommands<TCommand>
    {
        private readonly Func<IHandleCommands<TCommand>> _instanceCreator;

        public RunAsyncCommandHandlerProxy(Func<IHandleCommands<TCommand>> instanceCreator)
        {
            _instanceCreator = instanceCreator;
        }

        public void Handle(TCommand command)
        {
            Task.Factory.StartNew(() =>
            {
                var handler = _instanceCreator();
                handler.Handle(command);
            });
        }
    }
}
