using System;

namespace UCosmic.Domain.Files
{
    public class FileFactory : FileFinder
    {
        private readonly ICommandObjects _objectCommander;

        public FileFactory(ICommandObjects objectCommander, IQueryEntities entityQueries)
            : base(entityQueries)
        {
            if (objectCommander == null) throw new ArgumentNullException("objectCommander");
            _objectCommander = objectCommander;
        }

        public LooseFile Create(byte[] content, string mimeType, string name)
        {
            if (content == null) throw new ArgumentNullException("content");
            if (string.IsNullOrWhiteSpace(mimeType)) throw new ArgumentException("Cannot be empty or whitespace.", "mimeType");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Cannot be empty or whitespace.", "name");
            if (content.Length < 1) throw new ArgumentException("Length must be greater than zero.", "content");

            var entity = new LooseFile
            {
                Content = content,
                Length = content.Length,
                MimeType = mimeType,
                Name = name,
            };

            _objectCommander.Insert(entity, true);

            // return file detached from context
            return FindOne(By<LooseFile>.EntityId(entity.EntityId));
        }

        public void Purge(Guid fileEntityId, bool saveChanges = false)
        {
            var file = FindOne(By<LooseFile>.EntityId(fileEntityId));
            if (file != null)
                _objectCommander.Delete(file, saveChanges);
        }
    }
}