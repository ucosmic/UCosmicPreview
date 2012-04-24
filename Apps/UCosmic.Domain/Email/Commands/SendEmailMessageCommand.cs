using UCosmic.Domain.People;

namespace UCosmic.Domain.Email
{
    public class SendEmailMessageCommand
    {
        public int PersonId { get; set; }
        public int MessageNumber { get; set; }
    }
}
