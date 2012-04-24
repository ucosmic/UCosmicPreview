using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class GetEmailMessageByPersonIdAndNumberQuery : BaseQuery, IDefineQuery<EmailMessage>
    {
        public int PersonId { get; set; }
        public int Number { get; set; }
    }
}
