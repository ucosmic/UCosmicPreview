using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Self
{
    public class EmailInfo
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "This is not a valid email address.")]
        public string Value { get; set; }

        public bool IsDefault { get; set; }
        public bool IsConfirmed { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PersonId { get; set; }
        public PersonForm Person { get; set; }
    }
}