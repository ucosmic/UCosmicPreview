using FluentValidation;

namespace UCosmic.Domain
{
    public class FluentValidationCommandDecorator<TCommand> : IHandleCommands<TCommand>
    {
        private readonly IHandleCommands<TCommand> _decorated;
        private readonly IValidator<TCommand> _validator;

        public FluentValidationCommandDecorator(IHandleCommands<TCommand> decorated, IValidator<TCommand> validator)
        {
            _decorated = decorated;
            _validator = validator;
        }

        public void Handle(TCommand command)
        {
            _validator.ValidateAndThrow(command);
            _decorated.Handle(command);
        }
    }
}
