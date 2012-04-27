using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class GetEmailMessageByNumberQuery : BaseQuery, IDefineQuery<EmailMessage>
    {
        public int PersonId { get; set; }
        public int Number { get; set; }
    }
}
