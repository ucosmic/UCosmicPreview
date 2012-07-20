using System.Net.Mail;

namespace UCosmic
{
    public interface ISendMail
    {
        void Send(MailMessage message);
    }
}