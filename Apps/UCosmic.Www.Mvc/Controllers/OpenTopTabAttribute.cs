using System;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OpenTopTabAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private const string KeyFormat = "{0}TopTabCssClass";
        private const string CssClass = "docked";
        private const string UvScriptKey = "UvScript";
        public const string UvHrefKey = "UvHref";
        public const string UvDefaultHref = "https://ucosmic.uservoice.com/forums/150533-general-feedback-support";
        public const string UvDefaultScript = "kKkVzX4Nj95jXT74LBo3VQ";

        public OpenTopTabAttribute(string name)
        {
            _name = name;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            OpenTopTab(filterContext.Controller.ViewData, _name);
        }

        public static void OpenTopTab(ViewDataDictionary viewData, string name)
        {
            viewData[string.Format(KeyFormat, name)] = CssClass;
            switch (name)
            {
                case TopTabName.InstitutionalAgreements:
                    viewData[UvScriptKey] = "foaDhAccc9p9GWHsSl81dg";
                    viewData[UvHrefKey] = "https://ucosmic.uservoice.com/forums/133860-institutional-agreements";
                    break;
                case TopTabName.FacultyStaff:
                    viewData[UvScriptKey] = "PhrFhNdbVBZ9V19hPg2w";
                    viewData[UvHrefKey] = "http://ucosmic.uservoice.com/forums/150532-faculty-staff";
                    break;
                case TopTabName.Alumni:
                    viewData[UvScriptKey] = "WtfS3tlQ2gVDkCKKPDPkQ";
                    viewData[UvHrefKey] = "https://ucosmic.uservoice.com/forums/150535-alumni";
                    break;
                case TopTabName.Students:
                    viewData[UvScriptKey] = "1ctpeEPPvhVh5y5NIbQ";
                    viewData[UvHrefKey] = "https://ucosmic.uservoice.com/forums/150536-students";
                    break;
                case TopTabName.Representatives:
                    viewData[UvScriptKey] = "ULe6KwpyqbIMj5HqPQWg";
                    viewData[UvHrefKey] = "https://ucosmic.uservoice.com/forums/150538-representatives";
                    break;
                case TopTabName.Travel:
                    viewData[UvScriptKey] = "TDVw1ZesXD3hLBUdmCPRSA";
                    viewData[UvHrefKey] = "https://ucosmic.uservoice.com/forums/150539-travel";
                    break;
                case TopTabName.CorporateEngagement:
                    viewData[UvScriptKey] = "glp1zwnv17VoHfrOD4gg";
                    viewData[UvHrefKey] = "https://ucosmic.uservoice.com/forums/150540-corporate-engagement";
                    break;
                case TopTabName.GlobalPress:
                    viewData[UvScriptKey] = "aGQxMkwUIfUlRfCLPTOx6A";
                    viewData[UvHrefKey] = "https://ucosmic.uservoice.com/forums/150541-global-press";
                    break;
                default:
                    viewData[UvScriptKey] = UvDefaultScript;
                    viewData[UvHrefKey] = UvDefaultHref;
                    break;
            }
        }
    }

    public static class TopTabName
    {
        public const string InstitutionalAgreements = "InstitutionalAgreements";
        public const string FacultyStaff = "FacultyStaff";
        public const string Alumni = "Alumni";
        public const string Students = "Students";
        public const string Representatives = "Representatives";
        public const string Travel = "Travel";
        public const string CorporateEngagement = "CorporateEngagement";
        public const string GlobalPress = "GlobalPress";
        //public const string User = "User";
        //public const string SignIn = "SignIn";
        //public const string SignUp = "SignUp";
        public const string Home = "Home";
    }
}
