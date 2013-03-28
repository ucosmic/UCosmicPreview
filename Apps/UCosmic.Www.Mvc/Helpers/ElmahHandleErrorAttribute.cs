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
            var exception = context.Exception;
            if (!context.ExceptionHandled || RaiseErrorSignal(exception) || IsFiltered(context))
                return;
            LogException(exception);
        }

        private static bool RaiseErrorSignal(Exception e)
        {
            var current = HttpContext.Current;
            if (current == null)
                return false;
            var errorSignal = ErrorSignal.FromContext(current);
            if (errorSignal == null)
                return false;
            errorSignal.Raise(e, current);
            return true;
        }

        private static bool IsFiltered(ExceptionContext context)
        {
            var filterConfiguration = context.HttpContext.GetSection("elmah/errorFilter") as ErrorFilterConfiguration;
            if (filterConfiguration == null)
                return false;
            var assertionHelperContext = new ErrorFilterModule.AssertionHelperContext(context.Exception, HttpContext.Current);
            return filterConfiguration.Assertion.Test(assertionHelperContext);
        }

        private static void LogException(Exception e)
        {
            var current = HttpContext.Current;
            ErrorLog.GetDefault(current).Log(new Error(e, current));
        }
    }
}