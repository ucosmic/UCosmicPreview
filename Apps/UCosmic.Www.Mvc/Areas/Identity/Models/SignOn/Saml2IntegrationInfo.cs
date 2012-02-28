namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignOn
{
    public class Saml2IntegrationInfo
    {
        public string OfficialName { get; set; }
        public EmailDomainInfo[] EmailDomains { get; set; }
        public class EmailDomainInfo
        {
            public string Value { get; set; }
        }
    }
}
