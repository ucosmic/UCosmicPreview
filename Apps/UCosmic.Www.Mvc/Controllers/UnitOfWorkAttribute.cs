using System.Web.Mvc;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Controllers
{
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // do not save changes if there was an exception
            if (filterContext.Exception == null)
                UnitOfWork.SaveChanges();
            base.OnActionExecuted(filterContext);
        }
    }
}