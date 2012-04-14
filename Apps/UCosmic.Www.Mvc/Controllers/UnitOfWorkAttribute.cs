using System.Web.Mvc;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Controllers
{
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            UnitOfWork.SaveChanges();
            base.OnActionExecuted(filterContext);
        }
    }
}