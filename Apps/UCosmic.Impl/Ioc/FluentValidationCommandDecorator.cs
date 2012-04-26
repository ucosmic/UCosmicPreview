using FluentValidation;
using SimpleInjector;

namespace UCosmic.Impl
{
    public class FluentValidationCommandDecorator<TCommand> : IHandleCommands<TCommand>
    {
        private readonly IHandleCommands<TCommand> _decorated;
        //private readonly IValidator<TCommand> _validator;
        private readonly Container _container;

        public FluentValidationCommandDecorator(IHandleCommands<TCommand> decorated
            //, IValidator<TCommand> validator
            , Container container
        )
        {
            _decorated = decorated;
            //_validator = validator;
            _container = container;
        }

        public void Handle(TCommand command)
        {
            //_validator.ValidateAndThrow(command);

            IValidator<TCommand> validator = null;
            if (_container.GetRegistration(typeof(IValidator<TCommand>)) != null)
                validator = _container.GetInstance<IValidator<TCommand>>();

            if (validator != null) validator.ValidateAndThrow(command);

            _decorated.Handle(command);
        }
    }
}
