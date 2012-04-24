using System;
using System.Net.Mail;

namespace UCosmic
{
    public interface ISendMail : IDisposable
    {
        void Send(MailMessage message);
    }
}