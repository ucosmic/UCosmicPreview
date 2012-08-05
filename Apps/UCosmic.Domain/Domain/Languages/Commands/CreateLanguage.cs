using System;

namespace UCosmic.Domain.Languages
{
    public class CreateLanguage
    {
        public string TwoLetterIsoCode { get; set; }
        public string ThreeLetterIsoCode { get; set; }
        public string ThreeLetterIsoBibliographicCode { get; set; }
        public Language CreatedLanguage { get; internal set; }
    }

    public class HandleCreateLanguageCommand : IHandleCommands<CreateLanguage>
    {
        private readonly ICommandEntities _entities;

        public HandleCreateLanguageCommand(ICommandEntities entities)
        {
            _entities = entities;
        }

        public void Handle(CreateLanguage command)
        {
            if (command == null) throw new ArgumentNullException("command");

            var language = new Language
            {
                TwoLetterIsoCode = command.TwoLetterIsoCode,
                ThreeLetterIsoCode = command.ThreeLetterIsoCode,
                ThreeLetterIsoBibliographicCode = command.ThreeLetterIsoBibliographicCode,
            };

            _entities.Create(language);
            command.CreatedLanguage = language;
        }
    }
}
