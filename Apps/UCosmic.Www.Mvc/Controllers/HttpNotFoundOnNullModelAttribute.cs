using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Controllers
{
    public class HttpNotFoundOnNullModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResultBase = filterContext.Result as ViewResultBase;
            if (viewResultBase != null && viewResultBase.Model == null)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
            base.OnActionExecuted(filterContext);
        }
    }
}