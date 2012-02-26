using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Controllers
{
    public abstract partial class BaseController : Controller
    {
        public const string FeedbackMessageKey = "TopMessage";
        protected const string SharedName = "Shared";

        [NonAction]
        protected static string GetEditorTemplateViewName(string areaName, string controllerName, string t4ViewName)
        {
            return string.Format(
                "~/Areas/{0}/Views/{1}/EditorTemplates/{2}.cshtml",
                areaName, controllerName, t4ViewName);
        }

        // ReSharper disable UnusedMember.Global
        [NonAction]
        protected static string GetEditorTemplateViewName(string t4ViewName, string controllerName)
        {
            return string.Format(
                "~/Views/{0}/EditorTemplates/{1}.cshtml",
                controllerName, t4ViewName);
        }
        // ReSharper restore UnusedMember.Global

        [NonAction]
        protected void SetFeedbackMessage(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                TempData.FeedbackMessage(message);
            }
        }
    }

}