using FluentValidation;

namespace UCosmic.Domain
{
    public class FluentValidationCommandDecorator<TCommand> : IHandleCommands<TCommand>
    {
        private readonly IHandleCommands<TCommand> _decorated;
        private readonly IValidator<TCommand> _validator;

        public FluentValidationCommandDecorator(IValidator<TCommand> validator
            , IHandleCommands<TCommand> decorated
        )
        {
            _validator = validator;
            _decorated = decorated;
        }

        public void Handle(TCommand command)
        {
            _validator.ValidateAndThrow(command);

            _decorated.Handle(command);
        }
    }
}
