using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class PublicSearchRouteMapper
    {
        private static readonly string Area = MVC.InstitutionalAgreements.Name;
        private static readonly string Controller = MVC.InstitutionalAgreements.PublicSearch.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(PublicSearchRouteMapper), context, Area, Controller);
        }

        public static class Index
        {
            public const string Route = "{establishmentUrl}/institutional-agreements/{keyword}";
            private static readonly string Action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.Index;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                    establishmentUrl = "my",
                    keyword = UrlParameter.Optional,
                };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class GetChildEstablishmentsWithAgreements
        {
            public const string Route = "{establishmentUrl}/institutional-agreements/under-parent/{parentId}";
            private static readonly string Action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.GetChildEstablishmentsWithAgreements;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                    establishmentUrl = "my",
                };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    parentId = new NonEmptyGuidRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class ChangeOwner
        {
            public const string Route = "institutional-agreements/change-owner";
            private static readonly string Action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.ChangeOwner;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class Info
        {
            public const string Route = "institutional-agreements/{agreementId}";
            private static readonly string Action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.Info;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new
                {
                    area,
                    controller,
                    action = Action,
                    establishmentUrl = "my",
                };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                    agreementId = new NonEmptyGuidRouteConstraint(),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class DisplayFile
        {
            public const string Route = "institutional-agreements/files/{fileId}/display/{fileName}";
            private static readonly string Action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.DisplayFile;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class DownloadFile
        {
            public const string Route = "institutional-agreements/files/{fileId}/download/{fileName}";
            private static readonly string Action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.DownloadFile;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class AutoCompleteKeyword
        {
            public const string Route = "institutional-agreements/autocomplete/search/keyword/{establishmentUrl}";
            private static readonly string Action = MVC.InstitutionalAgreements.PublicSearch.ActionNames.AutoCompleteKeyword;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("POST"),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

    }
    // ReSharper restore UnusedMember.Global
}