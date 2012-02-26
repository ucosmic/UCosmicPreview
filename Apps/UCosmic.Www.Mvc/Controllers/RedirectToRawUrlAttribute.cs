using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Controllers
{
    public class RedirectToRawUrlAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var redirectResult = filterContext.Result as RedirectResult;
            if (redirectResult == null) return;

            var originalUrl = redirectResult.Url;
            if (string.IsNullOrWhiteSpace(originalUrl))
            {
                originalUrl = string.Empty;
            }
            if (!originalUrl.StartsWith("/"))
            {
                filterContext.Result = new RedirectResult(string.Format("/{0}", originalUrl));
            }
        }
    }
}