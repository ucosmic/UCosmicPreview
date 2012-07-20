using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace UCosmic.Impl
{
    public class SmtpMailSender : ISendMail
    {
        private readonly IManageConfigurations _configurationManager;
        private readonly ILogExceptions _exceptionLogger;

        public SmtpMailSender(IManageConfigurations configurationManager
            , ILogExceptions exceptionLogger
        )
        {
            _configurationManager = configurationManager;
            _exceptionLogger = exceptionLogger;
        }

        public void Send(MailMessage message)
        {
            Send(message, 0);
        }

        private void Send(MailMessage message, int retryCount)
        {
            if (message == null) throw new ArgumentNullException("message");

            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    // in development & qa, deliver mail to test mail server folder
                    if (smtpClient.DeliveryMethod == SmtpDeliveryMethod.SpecifiedPickupDirectory)
                    {
                        var path = Path.Combine(HttpRuntime.AppDomainAppPath, _configurationManager.TestMailServer);
                        var directory = Directory.CreateDirectory(path);
                        smtpClient.PickupDirectoryLocation = directory.FullName;
                    }

                    // rename recipients when not deployed to prevent sending to unwanted recipients
                    if (!_configurationManager.IsDeployedToCloud)
                    {
                        var toAddress = message.To.First().Address;
                        message.To.Clear();
                        message.CC.Clear();
                        message.Bcc.Clear();
                        foreach (var interceptAddress in _configurationManager.EmailInterceptAddresses.Explode(";"))
                            message.To.Add(new MailAddress(interceptAddress, string.Format(
                                "Intended for {0} (UCosmic Mail Intercept)", toAddress)));
                    }

                    // send the message
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                // log the exception
                _exceptionLogger.LogException(ex);

                // give up after trying 3 times
                if (++retryCount > 2) throw;

                // wait 3 seconds and try to send the message again
                Thread.Sleep(3000);
                Send(message, retryCount);
            }
        }
    }
}
