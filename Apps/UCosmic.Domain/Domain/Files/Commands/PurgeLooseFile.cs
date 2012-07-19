using System;

namespace UCosmic.Domain.Files
{
    public class PurgeLooseFileCommand
    {
        public PurgeLooseFileCommand(Guid guid)
        {
            if (guid == Guid.Empty) throw new ArgumentException("Cannot be empty", "guid");
            Guid = guid;
        }

        public Guid Guid { get; private set; }
    }

    public class PurgeLooseFileHandler : IHandleCommands<PurgeLooseFileCommand>
    {
        private readonly IProcessQueries _queryProcessor;
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;

        public PurgeLooseFileHandler(IProcessQueries queryProcessor, ICommandEntities entities, IUnitOfWork unitOfWork)
        {
            _queryProcessor = queryProcessor;
            _entities = entities;
            _unitOfWork = unitOfWork;
        }

        public void Handle(PurgeLooseFileCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var entity = _queryProcessor.Execute(
                new GetLooseFileByGuidQuery(command.Guid));

            if (entity == null) return;

            _entities.Purge(entity);
            _unitOfWork.SaveChanges();
        }
    }
}
