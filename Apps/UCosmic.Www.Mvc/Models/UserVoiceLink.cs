using System.Web;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Controllers;

namespace UCosmic.Www.Mvc.Models
{
    public class UserVoiceLink
    {
        private readonly bool _javascriptPopup;
        private readonly ViewDataDictionary _viewData;
        public UserVoiceLink(HttpRequestBase request, ViewDataDictionary viewData)
        {
            Text = "Feedback & Support";
            _javascriptPopup = WebConfig.EnableUserVoiceWidget(request);
            _viewData = viewData;
        }
        public string CssClass { get; set; }
        public string Text { get; set; }
        public string TextAfter { get; set; }
        public string Dialog { get; set; }
        public string Href
        {
            get
            {
                if (_javascriptPopup) return "javascript:UserVoice.showPopupWidget();";
                if (_viewData[OpenTopTabAttribute.UvHrefKey] != null)
                {
                    return _viewData[OpenTopTabAttribute.UvHrefKey].ToString();
                }
                return OpenTopTabAttribute.UvDefaultHref;
            }
        }
        public string Title
        {
            get { return (_javascriptPopup) ? "Open feedback & support dialog (powered by UserVoice)" : null; }
        }
        public string Target
        {
            get { return (_javascriptPopup) ? null : "_blank"; }
        }
    }
}