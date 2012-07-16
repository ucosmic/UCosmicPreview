using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Areas.Common.Controllers
{
    public partial class QaController : Controller
    {
        private const string EmailFileSearchPattern = "*.eml";
        private readonly IManageConfigurations _config;

        public QaController(IManageConfigurations config)
        {
            _config = config;
        }

        [ActionName("qa-mail-delivery")]
        public virtual ActionResult DeliverQaMail()
        {
            var mailServerPath = Path.Combine(HttpRuntime.AppDomainAppPath, _config.TestMailServer);
            var mailInboxPath = Path.Combine(HttpRuntime.AppDomainAppPath, _config.TestMailInbox);
            var mailServer = Directory.CreateDirectory(mailServerPath);
            var mailInbox = Directory.CreateDirectory(mailInboxPath);
            var mailFiles = mailServer.GetFileSystemInfos(EmailFileSearchPattern);
            var model = new List<string>();
            foreach (var file in mailFiles)
            {
                var deliveryPath = Path.Combine(mailInbox.FullName, file.Name);
                System.IO.File.Move(file.FullName, deliveryPath);
                model.Add(file.Name);
            }
            return View(MVC.Common.Qa.Views.mail_files_names, model);
        }

        [ActionName("qa-mail-reset")]
        public virtual ActionResult ResetQaMail()
        {
            var model = new List<string>();
            var mailInboxPath = Path.Combine(HttpRuntime.AppDomainAppPath, _config.TestMailInbox);
            var mailInbox = Directory.CreateDirectory(mailInboxPath);
            var files = mailInbox.GetFileSystemInfos(EmailFileSearchPattern);
            foreach (var file in files)
            {
                System.IO.File.Delete(file.FullName);
                model.Add(file.Name);
            }
            return View(MVC.Common.Qa.Views.mail_files_names, model);
        }

    }
}
