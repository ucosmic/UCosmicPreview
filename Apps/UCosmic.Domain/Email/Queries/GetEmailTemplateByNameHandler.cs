namespace UCosmic.Domain.Email
{
    public class GetEmailTemplateByNameHandler : IHandleQueries<GetEmailTemplateByNameQuery, EmailTemplate>
    {
        private readonly IQueryEntities _entities;

        public GetEmailTemplateByNameHandler(IQueryEntities entities)
        {
            _entities = entities;
        }

        public EmailTemplate Handle(GetEmailTemplateByNameQuery query)
        {
            // get the template
            var template = _entities.EmailTemplates
                .ByName(query.Name, query.EstablishmentId)
            ;

            // fall back to default
            if (template == null && query.EstablishmentId.HasValue)
                template = _entities.EmailTemplates
                    .ByName(query.Name, null)
                ;

            return template;
        }
    }
}
