using System.ComponentModel.DataAnnotations;
using UCosmic.Www.Mvc.Areas.My.Controllers;
using UCosmic.Www.Mvc.Areas.People.Controllers;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    public class UpdateNameForm
    {
        private const string NoneNullDisplayText = PersonNameController.SalutationAndSuffixNullValueLabel;
        private const string UnknownNullDisplayText = "[Unknown]";

        public const string DisplayNameDisplayName = "Display name";
        public const string DisplayNameDisplayPrompt = "Please enter a Display name.";
        [Display(Name = DisplayNameDisplayName, Prompt = DisplayNameDisplayPrompt)]
        public string DisplayName { get; set; }

        public const string IsDisplayNameDerivedDisplayName = "Automatically generate my display name based on the fields below.";
        [Display(Name = IsDisplayNameDerivedDisplayName)]
        public bool IsDisplayNameDerived { get; set; }

        public const string SalutationDisplayName = "Salutation";
        public const string SalutationNullDisplayText = NoneNullDisplayText;
        [Display(Name = SalutationDisplayName)]
        [DisplayFormat(NullDisplayText = SalutationNullDisplayText)]
        public string Salutation { get; set; }

        public const string FirstNameDisplayName = "First name";
        public const string FirstNameNullDisplayText = UnknownNullDisplayText;
        [Display(Name = FirstNameDisplayName)]
        [DisplayFormat(NullDisplayText = FirstNameNullDisplayText)]
        public string FirstName { get; set; }

        public const string MiddleNameDisplayName = "Middle name or initial";
        public const string MiddleNameNullDisplayText = NoneNullDisplayText;
        [Display(Name = MiddleNameDisplayName)]
        [DisplayFormat(NullDisplayText = MiddleNameNullDisplayText)]
        public string MiddleName { get; set; }

        public const string LastNameDisplayName = "Last name";
        public const string LastNameNullDisplayText = UnknownNullDisplayText;
        [Display(Name = LastNameDisplayName)]
        [DisplayFormat(NullDisplayText = LastNameNullDisplayText)]
        public string LastName { get; set; }

        public const string SuffixDisplayName = "Suffix";
        public const string SuffixNullDisplayText = NoneNullDisplayText;
        [Display(Name = SuffixDisplayName)]
        [DisplayFormat(NullDisplayText = SuffixNullDisplayText)]
        public string Suffix { get; set; }

        public static string ReturnUrl
        {
            get { return string.Format("~/{0}", ProfileRouter.Get.Route); }
        }
    }
}