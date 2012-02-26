namespace UCosmic.Domain.Email
{
    public class EmailTemplateQuery : RevisableEntityQueryCriteria<EmailTemplate>
    {
        public string Name { get; set; }
        public int? ForEstablishmentRevisionId { get; set; }
        public bool? FallBackToDefault { get; set; }
    }

    public static class EmailTemplateBy
    {
        public static EmailTemplateQuery Name(string name,
            int? forEstablishmentRevisionId = null, bool? fallBackToDefault = null)
        {
            return new EmailTemplateQuery
            {
                Name = name,
                ForEstablishmentRevisionId = forEstablishmentRevisionId,
                FallBackToDefault = fallBackToDefault
            };
        }
    }
}