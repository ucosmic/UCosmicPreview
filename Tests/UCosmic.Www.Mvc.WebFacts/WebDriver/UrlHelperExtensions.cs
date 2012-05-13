using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UCosmic.Www.Mvc
{
    public static class UrlHelperExtensions
    {
        private static UrlHelper _urlHelper;

        private static void InitializeUrlHelper()
        {
            if (_urlHelper.IsNotNull()) return;

            var httpRequest = new HttpRequest(string.Empty, "http://www.site.tld", null);
            var httpResponse = new HttpResponse(null);
            var httpContext = new HttpContext(httpRequest, httpResponse);
            var httpContextBase = new HttpContextWrapper(httpContext);
            var requestContext = new RequestContext(httpContextBase, new RouteData());
            _urlHelper = new UrlHelper(requestContext, RouteTable.Routes);
        }

        public static string AsPath(this ActionResult actionResult)
        {
            InitializeUrlHelper();
            return _urlHelper.Action(actionResult);
        }
    }
}