using System;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Web;
using UCosmic.Domain;
using UCosmic.Domain.People;

namespace UCosmic.Impl
{
    public class SmtpEmailSender
    {
        private readonly EmailMessage _emailMessage;
        private readonly ICommandObjects _objectCommander;
        private readonly IManageConfigurations _config;
        private int _retryCount;

        public SmtpEmailSender(ICommandObjects objectCommander, EmailMessage emailMessage, IManageConfigurations config)
        {
            if (emailMessage == null)
                throw new ArgumentNullException("emailMessage");

            _emailMessage = emailMessage;
            _objectCommander = objectCommander;
            _config = config;
        }

        internal void Send()
        {
            // check whether the message has been sent
            var sent = _emailMessage.SentOnUtc.HasValue;
            if (sent) return;

            try
            {
                // initialize message
                var message = new MailMessage
                {
                    From = new MailAddress(_emailMessage.FromAddress, _emailMessage.FromDisplayName),
                    Subject = _emailMessage.Subject,
                    Body = _emailMessage.Body,
                };

                // use the EmailInterceptAddresses app setting when it is configured
                var interceptAddresses = _config.EmailInterceptAddresses;
                if (string.IsNullOrWhiteSpace(interceptAddresses))
                {
                    // wait for previous unit of work to commit
                    Thread.Sleep(15000);

                    message.To.Add(new MailAddress(_emailMessage.ToAddress,
                        _emailMessage.ToPerson.DisplayName));
                }
                else
                {
                    foreach (var interceptAddress in interceptAddresses.Explode(";"))
                    {
                        message.To.Add(new MailAddress(interceptAddress,
                            string.Format("UCosmic Intercept (intended for {0})",
                                _emailMessage.ToAddress)));
                    }
                }
                // reply-to address
                if (!string.IsNullOrWhiteSpace(_emailMessage.ReplyToAddress))
                {
                    message.ReplyToList.Add(new MailAddress(_emailMessage.ReplyToAddress, _emailMessage.FromAddress));
                }

                // wait for previous unit of work to commit
                //while (_emailMessage.Id == 0)
                //{
                //Thread.Sleep(15000);
                //}

                // send the message
                var client = new SmtpClient();
                if (!_config.IsDeployedToCloud)
                {
                    // for development & qa, deliver mail to test mail server folder
                    var path = Path.Combine(HttpRuntime.AppDomainAppPath, _config.TestMailServer);
                    var directory = Directory.CreateDirectory(path);
                    client.PickupDirectoryLocation = directory.FullName;
                }
                client.Send(message);
                sent = true;
                var sentOnUtc = DateTime.UtcNow;

                // record the date & time the email was sent
                _emailMessage.SentOnUtc = sentOnUtc;
                _objectCommander.Update(_emailMessage.ToPerson, true);
                //_people.Refresh(_emailMessage.To.Person);
                //_people.UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                // log the exception
                var exceptionLogger = DependencyInjector.Current.GetService<ILogExceptions>();
                exceptionLogger.LogException(ex);

                // wait 10 seconds and try to send the message again
                if (!sent && _retryCount++ <= 3)
                {
                    Thread.Sleep(10000);
                    Send();
                }
            }
        }
    }
}