using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Domain;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ConfigurationForms
{
    public class InstitutionalAgreementContactTypeValueForm : IHaveText
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ConfigurationId { get; set; }

        [Display(Name = TextDisplayName)]
        [RequiredIfClient("IsAdded", ComparisonType.IsEqualTo, true, ErrorMessage = TextRequiredErrorFormat)]
        [StringLength(150, ErrorMessage = "{0} cannot contain more than {1} characters.")]
        public string Text { get; set; }
        public const string TextDisplayName = "Contact type";
        public const string TextRequiredErrorFormat = "Please enter a {0} option.";

        [HiddenInput(DisplayValue = false)]
        public bool IsAdded { get; set; }
    }
}