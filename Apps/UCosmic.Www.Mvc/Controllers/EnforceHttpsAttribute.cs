using System;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Controllers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class EnforceHttpsAttribute : RequireHttpsAttribute
    {
        #region Implementation for AppHarbor

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (WebConfig.IsDeployedTo(DeployToTarget.Test))
            {
                if (filterContext == null) throw new ArgumentNullException("filterContext");

                if (filterContext.HttpContext.Request.IsSecureConnection) return;

                if (string.Equals(filterContext.HttpContext.Request.Headers["X-Forwarded-Proto"],
                    "https", StringComparison.InvariantCultureIgnoreCase)) return;

                if (filterContext.HttpContext.Request.IsLocal) return;

                HandleNonHttpsRequest(filterContext);
            }
            else
            {
                base.OnAuthorization(filterContext);
            }
        }

        #endregion
        #region Implementation with static cache callback handler

        //private static bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    return httpContext.Request.IsSecureConnection;
        //}

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    if (!AuthorizeCore(filterContext.HttpContext))
        //    {
        //        HandleNonHttpsRequest(filterContext);
        //    }
        //    else
        //    {
        //        var cache = filterContext.HttpContext.Response.Cache;
        //        cache.SetProxyMaxAge(new TimeSpan(0L));
        //        cache.AddValidationCallback(CacheValidateHandler, null);
        //    }
        //}

        //// ReSharper disable RedundantAssignment
        //private static void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        //// ReSharper restore RedundantAssignment
        //{
        //    validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        //}

        //private static HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        //{
        //    return !httpContext.Request.IsSecureConnection
        //        ? HttpValidationStatus.IgnoreThisRequest
        //        : HttpValidationStatus.Valid;
        //}

        #endregion
    }
}