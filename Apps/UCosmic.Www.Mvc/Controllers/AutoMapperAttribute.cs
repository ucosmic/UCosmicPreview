using System;
using System.Web.Mvc;
using AutoMapper;

namespace UCosmic.Www.Mvc.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutoMapperAttribute : ActionFilterAttribute
    {
        private Type DestinationType { get; set; }

        public AutoMapperAttribute(Type destType)
        {
            DestinationType = destType;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var jsonResult = filterContext.Result as JsonResult;
            if (jsonResult != null && jsonResult.Data != null)
            {
                var jsonData = Mapper.Map(jsonResult.Data, jsonResult.Data.GetType(), DestinationType);
                jsonResult.Data = jsonData;
                return;
            }

            var model = filterContext.Controller.ViewData.Model;
            if (model == null) return;

            var viewModel = Mapper.Map(model, model.GetType(), DestinationType);
            filterContext.Controller.ViewData.Model = viewModel;
        }
    }
}