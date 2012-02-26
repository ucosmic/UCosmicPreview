using UCosmic.Domain.People;

namespace UCosmic.Domain
{
    public interface ISendEmails
    {
        void Send(EmailMessage message);
    }
}