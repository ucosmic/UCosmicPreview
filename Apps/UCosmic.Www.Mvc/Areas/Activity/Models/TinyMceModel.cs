using System;
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

        [DataType(DataType.MultilineText)]
        [Display(Prompt = "[Enter your tag here]")]
        public string TagSearch { get; set; }

        public Tag[] Tags { get; set; }
        public class Tag
        {
            public int RevisionId { get; set; }
            public string TaggedText { get; set; }
            public Type DomainType { get; set; }
        }
    }
}