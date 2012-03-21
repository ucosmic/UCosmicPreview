using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UCosmic.Domain.Establishments
{
    public class DoSomethingCommandHandler : IHandleCommands<DoSomethingCommand>
    {
        private readonly ICommandEntities _entities;

        public DoSomethingCommandHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(DoSomethingCommand command)
        {
            //throw new NotImplementedException();
        }
    }
}
