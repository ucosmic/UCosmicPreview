using System;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace UCosmic
{
    public class SmtpMailSender : ISendMail
    {
        private bool _isDisposed;
        private int _retryCount;
        private SmtpClient _smtpClient;
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
            if (message == null) throw new ArgumentNullException("message");

            // do not send after disposal
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name,
                    "The mail sender has been disposed and can no longer be used.");

            // new up client in case exception was thrown by bad setup
            if (_smtpClient != null) _smtpClient.Dispose();
            _smtpClient = new SmtpClient();

            try
            {
                // for development & qa, deliver mail to test mail server folder
                if (!_configurationManager.IsDeployedToCloud)
                {
                    var path = Path.Combine(HttpRuntime.AppDomainAppPath, _configurationManager.TestMailServer);
                    var directory = Directory.CreateDirectory(path);
                    _smtpClient.PickupDirectoryLocation = directory.FullName;
                }
                _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                // send the message
                _smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                // log the exception
                _exceptionLogger.LogException(ex);

                // wait 10 seconds and try to send the message again
                if (_retryCount++ <= 3)
                {
                    Thread.Sleep(10000);
                    Send(message);
                }
                else throw;
            }

            Dispose();
        }

        public void Dispose()
        {
            if (_smtpClient != null) _smtpClient.Dispose();
            _isDisposed = true;
        }
    }
}
