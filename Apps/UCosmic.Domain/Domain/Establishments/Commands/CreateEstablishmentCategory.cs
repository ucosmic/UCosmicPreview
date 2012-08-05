using System;
using Pluralize;

namespace UCosmic.Domain.Establishments
{
    public class CreateEstablishmentCategory
    {
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string PluralEnglishName { get; set; }
    }

    public class HandleCreateEstablishmentCategoryCommand : IHandleCommands<CreateEstablishmentCategory>
    {
        private readonly ICommandEntities _entities;

        public HandleCreateEstablishmentCategoryCommand(ICommandEntities entities)
        {
            _entities = entities;

            Pluralizer.Instance.Learn("Business or Corporation", "Businesses & Corporations");
        }

        public void Handle(CreateEstablishmentCategory command)
        {
            if (command == null) throw new ArgumentNullException("command");

            if (string.IsNullOrWhiteSpace(command.PluralEnglishName))
            {
                const int pluralNumber = 2;
                var template = string.Format("{{{0}}}", command.EnglishName);

                // ReSharper disable PossiblyMistakenUseOfParamsMethod
                command.PluralEnglishName = Pluralizer.Instance.Pluralize(template, pluralNumber);
                // ReSharper restore PossiblyMistakenUseOfParamsMethod
            }

            var entity = new EstablishmentCategory
            {
                Code = command.Code,
                EnglishName = command.EnglishName,
                EnglishPluralName = command.PluralEnglishName,
            };

            _entities.Create(entity);
        }
    }
}
