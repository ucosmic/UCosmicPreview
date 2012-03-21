namespace UCosmic.Domain
{
    public interface IHandleCommands<TCommand>
    {
        void Handle(TCommand command);
    }
}