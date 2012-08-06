using System;

namespace UCosmic.Domain.Languages
{
    public class CreateLanguageName
    {
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public int TranslationToLanguageId { get; set; }
    }

    public class HandleCreateLanguageNameCommand : IHandleCommands<CreateLanguageName>
    {
        private readonly ICommandEntities _entities;

        public HandleCreateLanguageNameCommand(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(CreateLanguageName command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var language = _entities.FindByPrimaryKey<Language>(command.LanguageId);

            language.Names.Add(new LanguageName
            {
                Number = language.Names.NextNumber(),
                Text = command.Text,
                TranslationToLanguageId = command.TranslationToLanguageId,
            });

            _entities.Update(language);
        }
    }
}
