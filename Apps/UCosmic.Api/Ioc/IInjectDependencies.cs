using System;
using System.Collections.Generic;

namespace UCosmic
{
    public interface IInjectDependencies
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
    }

}
