using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.TestHelper;
using Should;

namespace UCosmic.Www.Mvc
{
    public static class ExtensionMethods
    {
        public static string ToAppRelativeUrl(this string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return "~/";

            var firstCharacter = url[0];
            switch (firstCharacter)
            {
                case '~':
                    return url;

                case '/':
                    return string.Format("~{0}", url);

                default:
                    return string.Format("~/{0}", url);
            }
        }

        public static string ToUrlHelperResult(this string url)
        {
            return url.ToAppRelativeUrl().Substring(1);
        }

        public static string AddQueryString(this string url, string format, params object[] values)
        {
            var queryString = string.Format(format, values);
            return string.Format("{0}{1}", url, queryString);
        }

        public static IEnumerable<RouteData> DefaultAreaRoutes<TController>(
            this Expression<Func<TController, ActionResult>> action, string areaName)
        {
            var methodCall = (MethodCallExpression)action.Body;
            var actionName1 = methodCall.Method.ActionName();
            var actionName2 = methodCall.Method.Name;
            var controllerName = typeof(TController).Name.Replace("Controller", "");
            var routeDatas = new List<RouteData>();
            var areaSegment = (!string.IsNullOrWhiteSpace(areaName)) ? areaName + "/" : string.Empty;

            if (actionName1.Equals("Index", StringComparison.OrdinalIgnoreCase))
                routeDatas.AddRange(string.Format("~/{0}{1}", areaSegment, controllerName)
                    .ToAppRelativeUrl().WithAnyMethod());

            routeDatas.AddRange(string.Format("~/{0}{1}/{2}", areaSegment, controllerName, actionName1)
                .ToAppRelativeUrl().WithAnyMethod());

            routeDatas.AddRange(string.Format("~/{0}{1}/{2}/Id", areaSegment, controllerName, actionName1)
                .ToAppRelativeUrl().WithAnyMethod());

            if (!actionName1.Equals(actionName2))
            {
                routeDatas.AddRange(string.Format("~/{0}{1}/{2}", areaSegment, controllerName, actionName2)
                    .ToAppRelativeUrl().WithAnyMethod());

                routeDatas.AddRange(string.Format("~/{0}{1}/{2}/Id", areaSegment, controllerName, actionName2)
                    .ToAppRelativeUrl().WithAnyMethod());
            }

            return routeDatas;
        }

        public static IEnumerable<RouteData> DefaultAreaRoutes(this string areaName, string controllerName = null)
        {
            var routeDatas = new List<RouteData>();

            var url = string.Format("~/{0}", areaName);
            if (!string.IsNullOrWhiteSpace(controllerName))
                url = string.Format("{0}/{1}", url, controllerName);

            routeDatas.AddRange(url.WithAnyMethod());

            return routeDatas;
        }

        public static IEnumerable<RouteData> WithMethodsExcept(this string url, params HttpVerbs[] httpVerbs)
        {
            return url.IncludeOrExcludeMethods(false, httpVerbs);
        }

        public static IEnumerable<RouteData> WithMethods(this string url, params HttpVerbs[] httpVerbs)
        {
            return url.IncludeOrExcludeMethods(true, httpVerbs);
        }

        private static IEnumerable<RouteData> IncludeOrExcludeMethods(this string url, bool include, params HttpVerbs[] httpVerbs)
        {
            var routeDatas = new List<RouteData>();

            if (httpVerbs.Contains(HttpVerbs.Get) == include)
                routeDatas.Add(url.WithMethod(HttpVerbs.Get));

            if (httpVerbs.Contains(HttpVerbs.Post) == include)
                routeDatas.Add(url.WithMethod(HttpVerbs.Post));

            if (httpVerbs.Contains(HttpVerbs.Put) == include)
                routeDatas.Add(url.WithMethod(HttpVerbs.Put));

            if (httpVerbs.Contains(HttpVerbs.Delete) == include)
                routeDatas.Add(url.WithMethod(HttpVerbs.Delete));

            if (httpVerbs.Contains(HttpVerbs.Head) == include)
                routeDatas.Add(url.WithMethod(HttpVerbs.Head));

            return routeDatas;
        }

        public static IEnumerable<RouteData> WithAnyMethod(this string url)
        {
            return new List<RouteData>
            {
                url.WithMethod(HttpVerbs.Get),
                url.WithMethod(HttpVerbs.Post),
                url.WithMethod(HttpVerbs.Put),
                url.WithMethod(HttpVerbs.Delete),
                url.WithMethod(HttpVerbs.Head),
            };
        }

        public static RouteData AndMethodArg(this RouteData routeData, string key, object value)
        {
            routeData.Values.Add(key, value);
            return routeData;
        }

        public static void ShouldMapToNothing(this RouteData routeData)
        {
            routeData.ShouldBeNull();
        }

        public static void ShouldMapToNothing(this IEnumerable<RouteData> routeDatas)
        {
            foreach (var routeData in routeDatas) routeData.ShouldMapToNothing();
        }

        public static void ShouldMapTo<TController>(this IEnumerable<RouteData> routeDatas,
            Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            foreach (var routeData in routeDatas) routeData.ShouldMapTo(action);
        }

        public static string WithoutTrailingSlash(this string value)
        {
            if (value.LastIndexOf("/", StringComparison.Ordinal) == value.Length - 1)
                value = value.Substring(0, value.Length - 1);

            return value;
        }

    }

    public static class OutBoundRoute
    {
        public static OutBoundRouteContext Of<TController>(Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            var controllerName = typeof(TController).Name.Replace("Controller", "");
            var methodCall = ((MethodCallExpression)action.Body);
            var methodName = methodCall.Method.ActionName();
            var routeValues = new RouteValueDictionary();

            for (var i = 0; i < methodCall.Arguments.Count; i++)
            {
                var name = methodCall.Method.GetParameters()[i].Name;
                object value = null;

                switch (methodCall.Arguments[i].NodeType)
                {
                    case ExpressionType.Constant:
                        value = ((ConstantExpression)methodCall.Arguments[i]).Value;
                        break;

                    case ExpressionType.MemberAccess:
                    case ExpressionType.Convert:
                        value = Expression.Lambda(methodCall.Arguments[i]).Compile().DynamicInvoke();
                        break;

                }

                routeValues.Add(name, value);
            }

            return new OutBoundRouteContext(controllerName, methodName, routeValues);
        }
    }

    public class OutBoundRouteContext
    {
        private string _area;
        private HttpVerbs? _httpMethod;
        private readonly string _action;
        private readonly string _controller;
        private readonly RouteValueDictionary _routeValues;
        private readonly RouteValueDictionary _viewModelParams = new RouteValueDictionary();

        public OutBoundRouteContext(string controllerName, string actionName, RouteValueDictionary routeValues)
        {
            _action = actionName;
            _controller = controllerName;
            _routeValues = routeValues;
        }

        public OutBoundRouteContext InArea(string area)
        {
            _area = area;
            return this;
        }

        public OutBoundRouteContext WithMethod(HttpVerbs httpMethod)
        {
            _httpMethod = httpMethod;
            return this;
        }

        public OutBoundRouteContext HavingViewModelProperty(string key, object value)
        {
            if (!_viewModelParams.ContainsKey(key))
                _viewModelParams.Add(key, value);
            return this;
        }

        private string Url()
        {
            var builder = ReuseMock.TestControllerBuilder(ControllerCustomization.ForUrlHelper);
            var context = new RequestContext(builder.HttpContext, new RouteData());

            var routeValues = new RouteValueDictionary();
            if (!string.IsNullOrWhiteSpace(_area))
                routeValues.Add("area", _area);
            routeValues.Add("controller", _controller);
            routeValues.Add("action", _action);
            if (_httpMethod.HasValue)
                routeValues.Add("httpMethod", _httpMethod.Value.ToString().ToUpper());
            if (!_httpMethod.HasValue || _httpMethod.Value != HttpVerbs.Post)
                foreach (var routeValue in _routeValues)
                    routeValues.Add(routeValue.Key, routeValue.Value);
            if (_viewModelParams != null)
                foreach (var viewModelParam in _viewModelParams)
                    routeValues.Add(viewModelParam.Key, viewModelParam.Value);

            var generatedUrl = UrlHelper.GenerateUrl(null, null, null, routeValues, RouteTable.Routes, context, true);

            return generatedUrl;
        }

        public string AppRelativeUrl()
        {
            var url = Url();
            return (url != null) ? url.ToAppRelativeUrl() : null;
        }
    }

}