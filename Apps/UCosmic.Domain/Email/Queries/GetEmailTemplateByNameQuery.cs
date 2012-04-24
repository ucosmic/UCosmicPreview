namespace UCosmic.Domain.Email
{
    public class GetEmailTemplateByNameQuery : IDefineQuery<EmailTemplate>
    {
        public string Name { get; set; }
        public int? EstablishmentId { get; set; }
    }
}
