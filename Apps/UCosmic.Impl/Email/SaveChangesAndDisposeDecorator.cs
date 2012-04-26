using UCosmic.Impl.Orm;

namespace UCosmic.Impl
{
    public class SaveChangesAndDisposeDecorator<TCommand> : IHandleCommands<TCommand>
    {
        private readonly IHandleCommands<TCommand> _decorated;
        private readonly UCosmicContext _dbContext;

        public SaveChangesAndDisposeDecorator(IHandleCommands<TCommand> decorated, UCosmicContext dbContext)
        {
            _decorated = decorated;
            _dbContext = dbContext;
        }

        public void Handle(TCommand command)
        {
            _decorated.Handle(command);
            _dbContext.SaveChanges();
            SimpleHttpContextLifestyleExtensions.DisposeInstance<UCosmicContext>();
        }
    }
}
