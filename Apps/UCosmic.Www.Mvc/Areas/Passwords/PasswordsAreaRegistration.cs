using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Passwords.Controllers;
using UCosmic.Www.Mvc.Areas.Passwords.Models;

namespace UCosmic.Www.Mvc.Areas.Passwords
{
    public class PasswordsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "passwords"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            ForgotPasswordRouter.RegisterRoutes(context);
            ForgotPasswordProfiler.RegisterProfiles();

            ResetPasswordRouter.RegisterRoutes(context);
            ResetPasswordProfiler.RegisterProfiles();

            //context.MapRoute(
            //    "Passwords_default",
            //    "Passwords/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
