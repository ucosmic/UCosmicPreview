using System.Net.Mail;

namespace UCosmic.Domain
{
    public interface ISendMail
    {
        void Send(MailMessage message);
    }
}