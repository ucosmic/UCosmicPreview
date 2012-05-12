using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Controllers
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
        Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthenticateAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly object _typeId = new object();

        public override object TypeId
        {
            get { return _typeId; }
        }

        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            // If a child action cache block is active, we need to fail immediately, even if authorization
            // would have succeeded. The reason is that there's no way to hook a callback to rerun
            // authorization before the fragment is served from the cache, so we can't guarantee that this
            // filter will be re-run on subsequent requests.
            if (OutputCacheAttribute.IsChildActionCacheActive(filterContext))
                throw new InvalidOperationException("AuthorizeAttribute cannot be used within a child action caching block.");

            if (!AuthorizeCore(filterContext.HttpContext))
                HandleUnauthorizedRequest(filterContext);

            // ** IMPORTANT **
            // Since we're performing authorization at the action level, the authorization code runs
            // after the output caching module. In the worst case this could allow an authorized user
            // to cause the page to be cached, then an unauthorized user would later be served the
            // cached page. We work around this by telling proxies not to cache the sensitive page,
            // then we hook our custom authorization code into the caching mechanism so that we have
            // the final say on whether a page should be served from the cache.
            var cachePolicy = filterContext.HttpContext.Response.Cache;
            cachePolicy.SetProxyMaxAge(new TimeSpan(0));
            cachePolicy.AddValidationCallback(CacheValidateHandler, null);
        }

        // This method must be thread-safe since it is called by the thread-safe OnCacheAuthorization() method.
        private static bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");
            return httpContext.User.Identity.IsAuthenticated;
        }

        private static void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        // This method must be thread-safe since it is called by the caching module.
        private static HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");

            var isAuthorized = AuthorizeCore(httpContext);
            return (isAuthorized) ? HttpValidationStatus.Valid : HttpValidationStatus.IgnoreThisRequest;
        }

        protected virtual void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}
