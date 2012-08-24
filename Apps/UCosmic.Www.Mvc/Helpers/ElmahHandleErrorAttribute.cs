using System;
using System.Web;
using System.Web.Mvc;
using Elmah;

namespace UCosmic.Www.Mvc
{
    // copied from Elmah.Contrib.Mvc project to compensate for dependencies on Elmah core.
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ElmahHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            Exception exception = context.Exception;
            if (!context.ExceptionHandled || ElmahHandleErrorAttribute.RaiseErrorSignal(exception) || ElmahHandleErrorAttribute.IsFiltered(context))
                return;
            ElmahHandleErrorAttribute.LogException(exception);
        }

        private static bool RaiseErrorSignal(Exception e)
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
                return false;
            ErrorSignal errorSignal = ErrorSignal.FromContext(current);
            if (errorSignal == null)
                return false;
            errorSignal.Raise(e, current);
            return true;
        }

        private static bool IsFiltered(ExceptionContext context)
        {
            ErrorFilterConfiguration filterConfiguration = context.HttpContext.GetSection("elmah/errorFilter") as ErrorFilterConfiguration;
            if (filterConfiguration == null)
                return false;
            ErrorFilterModule.AssertionHelperContext assertionHelperContext = new ErrorFilterModule.AssertionHelperContext(context.Exception, (object)HttpContext.Current);
            return filterConfiguration.Assertion.Test((object)assertionHelperContext);
        }

        private static void LogException(Exception e)
        {
            HttpContext current = HttpContext.Current;
            ErrorLog.GetDefault(current).Log(new Elmah.Error(e, current));
        }
    }
}