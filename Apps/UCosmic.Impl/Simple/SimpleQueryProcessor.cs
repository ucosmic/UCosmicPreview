using System.Diagnostics;
using SimpleInjector;

namespace UCosmic.Impl
{
    sealed class SimpleQueryProcessor : IProcessQueries
    {
        private readonly Container _container;

        public SimpleQueryProcessor(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public TResult Execute<TResult>(IDefineQuery<TResult> query)
        {
            var handlerType = typeof(IHandleQueries<,>).MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = _container.GetInstance(handlerType);

            return handler.Handle((dynamic)query);
        }
    }
}
