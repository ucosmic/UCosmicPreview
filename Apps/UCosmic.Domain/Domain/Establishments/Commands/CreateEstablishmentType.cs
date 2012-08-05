using System;
using Pluralize;

namespace UCosmic.Domain.Establishments
{
    public class CreateEstablishmentType
    {
        public string EnglishName { get; set; }
        public string PluralEnglishName { get; set; }
        public string CategoryCode { get; set; }
    }

    public class HandleCreateEstablishmentTypeCommand : IHandleCommands<CreateEstablishmentType>
    {
        private readonly ICommandEntities _entities;

        public HandleCreateEstablishmentTypeCommand(ICommandEntities entities)
        {
            _entities = entities;

            Pluralizer.Instance.Learn("University Campus", "University Campuses");
        }

        public void Handle(CreateEstablishmentType command)
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

            var category = _entities.FindByPrimaryKey<EstablishmentCategory>(command.CategoryCode);
            var entity = new EstablishmentType
            {
                CategoryCode = category.Code,
                EnglishName = command.EnglishName.Replace(" Or ", " or "),
                EnglishPluralName = command.PluralEnglishName,
            };

            _entities.Create(entity);
        }
    }
}
