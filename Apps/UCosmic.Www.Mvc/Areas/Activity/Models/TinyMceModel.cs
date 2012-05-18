using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Activity.Models
{
    public class TinyMceModel
    {
        [DataType(DataType.MultilineText)]
        [Display(Prompt = "[Enter the title or main heading of your activity here]")]
        public string Title { get; set; }

        [AllowHtml]
        [UIHint("TinyMceContent")]
        public string Content { get; set; }
    }
}