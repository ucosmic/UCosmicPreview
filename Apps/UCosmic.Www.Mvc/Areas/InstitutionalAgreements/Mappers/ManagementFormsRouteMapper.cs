using System.Web.Mvc;
using System.Web.Routing;
using UCosmic.Www.Mvc.Mappers;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class ManagementFormsRouteMapper
    {
        private static readonly string Area = MVC.InstitutionalAgreements.Name;
        private static readonly string Controller = MVC.InstitutionalAgreements.ManagementForms.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouteMapper.RegisterRoutes(typeof(ManagementFormsRouteMapper), context, Area, Controller);
        }

        public static class Browse
        {
            public const string Route = "my/institutional-agreements/v1";
            private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Browse;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new { httpMethod = new HttpMethodConstraint("GET") };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        //public static class New
        //{
        //    //public static readonly string[] Routes = 
        //    //{
        //    //    "my/institutional-agreements/v1/new/then-return-to/{*returnUrl}",
        //    //    "my/institutional-agreements/v1/new",
        //    //};
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.New;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        var constraintsWithReturnUrl = new
        //        {
        //            returnUrl = new RawUrlCatchallConstraint(),
        //            httpMethod = new HttpMethodConstraint("GET"), 
        //        };
        //        var constraintsWithoutReturnUrl = new { httpMethod = new HttpMethodConstraint("GET") };
        //        context.MapRoute(null, Routes[0], defaults, constraintsWithReturnUrl);
        //        context.MapRoute(null, Routes[1], defaults, constraintsWithoutReturnUrl);
        //    }
        //}

        //public static class Edit
        //{
        //    public static readonly string[] Routes = 
        //    {
        //        "my/institutional-agreements/v1/{entityId}/edit/then-return-to/{*returnUrl}",
        //        "my/institutional-agreements/v1/{entityId}/edit",
        //    };
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Edit;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        var constraintsWithReturnUrl = new
        //        {
        //            entityId = new NonEmptyGuidConstraint(),
        //            returnUrl = new RawUrlCatchallConstraint(),
        //            httpMethod = new HttpMethodConstraint("GET"),
        //        };
        //        var constraintsWithoutReturnUrl = new
        //        {
        //            entityId = new NonEmptyGuidConstraint(),
        //            httpMethod = new HttpMethodConstraint("GET"),
        //        };
        //        context.MapRoute(null, Routes[0], defaults, constraintsWithReturnUrl);
        //        context.MapRoute(null, Routes[1], defaults, constraintsWithoutReturnUrl);
        //    }
        //}

        public static class Post
        {
            public const string RouteForPost = "my/institutional-agreements";
            public static readonly string[] RoutesForGet = 
            {
                "my/institutional-agreements/v1/{entityId}/edit",
                "my/institutional-agreements/v1/new",
            };
            private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Post;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraintsForEdit = new
                {
                    entityId = new NonEmptyGuidRouteConstraint(),
                    httpMethod = new HttpMethodConstraint("GET"),
                };
                context.MapRoute(null, RoutesForGet[0], defaults, constraintsForEdit);

                var constraintsForNew = new
                {
                    httpMethod = new HttpMethodConstraint("GET")
                };
                context.MapRoute(null, RoutesForGet[1], defaults, constraintsForNew);

                var constraintsForPost = new { httpMethod = new HttpMethodConstraint("POST") };
                context.MapRoute(null, RouteForPost, defaults, constraintsForPost);
            }
        }

        //public static class Put
        //{
        //    public const string Route = "my/institutional-agreements/{entityId}";
        //    private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.Put;
        //    public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
        //    {
        //        var defaults = new { area, controller, action = Action, };
        //        var constraints = new
        //        {
        //            entityId = new NonEmptyGuidConstraint(),
        //            httpMethod = new HttpMethodConstraint("PUT", "POST"),
        //        };
        //        context.MapRoute(null, Route, defaults, constraints);
        //    }
        //}

        public static class AddParticipant
        {
            public const string Route = "my/institutional-agreements/manage/add-participant.partial.html";
            private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AddParticipant;
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

        public static class AttachFile
        {
            public const string Route = "my/institutional-agreements/manage/attach-file.partial.html";
            private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AttachFile;
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

        public static class AddContact
        {
            public const string Route = "my/institutional-agreements/manage/add-contact-form.partial.html";
            //public const string RouteForPost = "my/institutional-agreements/manage/add-contact-item.partial.html";
            private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AddContact;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                var constraints = new
                {
                    httpMethod = new HttpMethodConstraint("GET", "POST"),
                };
                context.MapRoute(null, Route, defaults, constraints);
            }
        }

        public static class DeriveTitle
        {
            public const string Route = "my/institutional-agreements/derive-title.json";
            private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.DeriveTitle;
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

        public static class AutoCompleteEstablishmentNames
        {
            public const string Route = "institutional-agreements/autocomplete/official-name.json";
            private static readonly string Action = MVC.InstitutionalAgreements.ManagementForms.ActionNames.AutoCompleteEstablishmentNames;
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
    }
    // ReSharper restore UnusedMember.Global
}