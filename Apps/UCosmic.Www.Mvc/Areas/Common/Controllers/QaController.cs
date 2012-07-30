using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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

    public static class QaRouter
    {
        private static readonly string Area = MVC.Common.Name;
        private static readonly string Controller = MVC.Common.Qa.Name;

        public class DeliverQaMailRoute : Route
        {
            public DeliverQaMailRoute()
                : base("qa/deliver-mail",
                WebConfig.IsDeployedToCloud ? new StopRoutingHandler() as IRouteHandler : new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Qa.ActionNames.DeliverQaMail,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }

        public class ResetQaMailRoute : Route
        {
            public ResetQaMailRoute()
                : base("qa/reset-mail",
                WebConfig.IsDeployedToCloud ? new StopRoutingHandler() as IRouteHandler : new MvcRouteHandler())
            {
                DataTokens = new RouteValueDictionary(new { area = Area });
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller,
                    action = MVC.Common.Qa.ActionNames.ResetQaMail,
                    keyword = UrlParameter.Optional,
                });
                Constraints = new RouteValueDictionary(new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                });
            }
        }
    }
}
