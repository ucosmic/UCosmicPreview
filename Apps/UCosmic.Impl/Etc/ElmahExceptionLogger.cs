using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using Elmah;

namespace UCosmic
{
    // ReSharper disable ClassNeverInstantiated.Global
    public class ElmahExceptionLogger : ILogExceptions
    // ReSharper restore ClassNeverInstantiated.Global
    {
        private readonly IManageConfigurations _config;

        public ElmahExceptionLogger(IManageConfigurations config)
        {
            _config = config;
        }

        public void LogException(Exception exception)
        {
            var error = new Error(exception);

            // first try to post it to the Elmah log
            try
            {
                var log = ErrorLog.GetDefault(null);
                if (log != null)
                {
                    log.Log(error);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Failed to add exception to Elmah error logger: {0}", ex.Message), "Error");
            }

            // second try to send it via Elmah mail
            try
            {
                var writer = new StringWriter();
                var formatter = new ErrorMailHtmlFormatter();
                formatter.Format(writer, error);
                var message = new MailMessage(_config.EmailDefaultFromAddress,
                    _config.EmailEmergencyAddresses)
                {
                    Subject = error.Message.Replace("\r", string.Empty).Replace("\n", string.Empty),
                    IsBodyHtml = true,
                    Body = writer.ToString(),
                };

                var client = new SmtpClient();
                client.Send(message);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Failed to send Elmah exception email: {0}", ex.Message), "Error");
            }

        }
    }
}