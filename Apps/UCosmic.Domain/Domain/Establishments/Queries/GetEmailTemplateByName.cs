namespace UCosmic.Domain.Establishments
{
    public class GetEmailTemplateByNameQuery : IDefineQuery<EmailTemplate>
    {
        public string Name { get; set; }
        public int? EstablishmentId { get; set; }
    }

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
            var template = _entities.Query<EmailTemplate>()
                .ByName(query.Name, query.EstablishmentId)
            ;

            // fall back to default
            if (template == null && query.EstablishmentId.HasValue)
                template = _entities.Query<EmailTemplate>()
                    .ByName(query.Name, null)
                ;

            return template;
        }
    }
}
