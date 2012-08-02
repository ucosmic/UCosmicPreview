using System;

namespace UCosmic.Domain.Establishments
{
    public class CreateEmailTemplate
    {
        public string Name { get; set; }
        public int? EstablishmentId { get; set; }
        public string SubjectFormat { get; set; }
        public string Instructions { get; set; }
        public string BodyFormat { get; set; }
    }

    public class EmailTemplateCreateHandler : IHandleCommands<CreateEmailTemplate>
    {
        private readonly ICommandEntities _entities;

        public EmailTemplateCreateHandler(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(CreateEmailTemplate command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var entity = new EmailTemplate
            {
                Name = command.Name,
                EstablishmentId = command.EstablishmentId,
                SubjectFormat = command.SubjectFormat,
                Instructions = command.Instructions,
                BodyFormat = command.BodyFormat,
            };
            _entities.Create(entity);
        }
    }
}
