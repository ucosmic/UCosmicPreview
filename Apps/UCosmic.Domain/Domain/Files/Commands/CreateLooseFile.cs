using System;

namespace UCosmic.Domain.Files
{
    public class CreateLooseFileCommand
    {
        public byte[] Content { get; set; }
        public string Name { get; set; }
        public string MimeType { get; set; }
        public LooseFile CreatedLooseFile { get; internal set; }
    }

    public class CreateLooseFileHandler : IHandleCommands<CreateLooseFileCommand>
    {
        private readonly ICommandEntities _entities;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLooseFileHandler(ICommandEntities entities, IUnitOfWork unitOfWork)
        {
            _entities = entities;
            _unitOfWork = unitOfWork;
        }

        public void Handle(CreateLooseFileCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var entity = new LooseFile
            {
                Content = command.Content,
                Length = command.Content.Length,
                MimeType = command.MimeType,
                Name = command.Name,
            };

            _entities.Create(entity);
            _unitOfWork.SaveChanges();
            command.CreatedLooseFile = _entities.Read<LooseFile>()
                .By(entity.EntityId);
        }
    }
}
