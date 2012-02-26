using System.Web.Mvc;
using UCosmic.Domain;

namespace UCosmic.Www.Mvc.Controllers
{
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var unitOfWork = DependencyInjector.Current.GetService<IUnitOfWork>();
            unitOfWork.SaveChanges();
            base.OnActionExecuted(filterContext);
        }
    }
}