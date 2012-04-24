using FluentValidation;
using SimpleInjector;

namespace UCosmic
{
    public class FluentValidationCommandDecorator<TCommand> : IHandleCommands<TCommand>
    {
        private readonly IHandleCommands<TCommand> _decorated;
        private readonly Container _container;

        public FluentValidationCommandDecorator(Container container
            , IHandleCommands<TCommand> decorated
        )
        {
            _container = container;
            _decorated = decorated;
        }

        public void Handle(TCommand command)
        {
            IValidator<TCommand> validator = null;
            if (_container.GetRegistration(typeof(IValidator<TCommand>)) != null)
                validator = _container.GetInstance<IValidator<TCommand>>();

            if (validator != null) validator.ValidateAndThrow(command);

            _decorated.Handle(command);
        }
    }
}
