using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms
{
    public class InstitutionalAgreementContactTypeValueForm : IHaveText
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ConfigurationId { get; set; }

        [Display(Name = "Contact type")]
        [RequiredIf("IsAdded", true, ErrorMessage = "Please enter a {0} option.")]
        [StringLength(150, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        public string Text { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsAdded { get; set; }
    }
}