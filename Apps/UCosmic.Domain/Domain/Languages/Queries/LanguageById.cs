using System;

namespace UCosmic.Domain.Languages
{
    public class LanguageById : BaseEntityQuery<Language>, IDefineQuery<Language>
    {
        public LanguageById(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }

    public class HandleLanguageByIdQuery : IHandleQueries<LanguageById, Language>
    {
        private readonly ICommandEntities _entities;

        public HandleLanguageByIdQuery(ICommandEntities entities)
        {
            _entities = entities;
        }

        public Language Handle(LanguageById query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = _entities.Get<Language>()
                .EagerLoad(_entities, query.EagerLoad)
                .ById(query.Id)
            ;

            return result;
        }
    }
}
