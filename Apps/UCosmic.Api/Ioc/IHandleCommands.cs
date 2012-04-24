namespace UCosmic
{
    public interface IHandleCommands<in TCommand>
    {
        void Handle(TCommand command);
    }
}