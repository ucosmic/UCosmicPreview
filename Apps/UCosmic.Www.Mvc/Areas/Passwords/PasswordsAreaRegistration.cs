using System.Web.Mvc;
using UCosmic.Www.Mvc.Areas.Passwords.Controllers;

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
            CreatePasswordRouter.RegisterRoutes(context);
            ForgotPasswordRouter.RegisterRoutes(context);
            ResetPasswordRouter.RegisterRoutes(context);
            UpdatePasswordRouter.RegisterRoutes(context);

            //context.MapRoute(
            //    "Passwords_default",
            //    "Passwords/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
