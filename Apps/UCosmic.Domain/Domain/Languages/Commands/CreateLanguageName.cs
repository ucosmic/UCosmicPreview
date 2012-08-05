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

            var language = _entities.Get<Language>().By(command.LanguageId);
            var translationTo = _entities.Get<Language>().By(command.TranslationToLanguageId);

            language.Names.Add(new LanguageName
            {
                Text = command.Text,
                TranslationToLanguage = translationTo,
            });

            _entities.Update(language);
        }
    }
}
