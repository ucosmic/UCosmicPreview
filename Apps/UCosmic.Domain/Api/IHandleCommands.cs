namespace UCosmic.Domain
{
    public interface IHandleCommands<in TCommand>
    {
        void Handle(TCommand command);
    }
}