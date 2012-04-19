using System.Web.Mvc;
using UCosmic.Www.Mvc.Routes;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Mappers
{
    // ReSharper disable UnusedMember.Global
    public static class ConfigurationFormsRouteMapper
    {
        private static readonly string Area = MVC.InstitutionalAgreements.Name;
        private static readonly string Controller = MVC.InstitutionalAgreements.ConfigurationForms.Name;

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            DefaultRouter.RegisterRoutes(typeof(ConfigurationFormsRouteMapper), context, Area, Controller);
        }

        public static class Add
        {
            public const string Route = "my/institutional-agreements/configure/set-up.html";
            private static readonly string Action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.Add;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class Edit
        {
            public static readonly string[] Routes = 
            {
                "my/institutional-agreements/configure",
                "my/institutional-agreements/configure.html",
            };
            private static readonly string Action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.Edit;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoutes(null, Routes, defaults);
            }
        }

        public static class NewAgreementType
        {
            public const string Route = "my/institutional-agreements/configure/{configurationId}/new-type-option.partial.html";
            private static readonly string Action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.NewAgreementType;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class NewAgreementStatus
        {
            public const string Route = "my/institutional-agreements/configure/{configurationId}/new-status-option.partial.html";
            private static readonly string Action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.NewAgreementStatus;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class NewAgreementContactType
        {
            public const string Route = "my/institutional-agreements/configure/{configurationId}/new-contact-type-option.partial.html";
            private static readonly string Action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.NewAgreementContactType;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class AgreementTypeOptions
        {
            public const string Route = "my/institutional-agreements/configure/get-type-options.json";
            private static readonly string Action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.AgreementTypeOptions;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class AgreementStatusOptions
        {
            public const string Route = "my/institutional-agreements/configure/get-status-options.json";
            private static readonly string Action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.AgreementStatusOptions;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class AgreementContactTypeOptions
        {
            public const string Route = "my/institutional-agreements/configure/get-contact-type-options.json";
            private static readonly string Action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.AgreementContactTypeOptions;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }

        public static class ValidateDuplicateOption
        {
            public const string Route = "my/institutional-agreements/configure/validate-duplicate-option.json";
            private static readonly string Action = MVC.InstitutionalAgreements.ConfigurationForms.ActionNames.ValidateDuplicateOption;
            public static void MapRoutes(AreaRegistrationContext context, string area, string controller)
            {
                var defaults = new { area, controller, action = Action, };
                context.MapRoute(null, Route, defaults);
            }
        }
    }
    // ReSharper restore UnusedMember.Global
}