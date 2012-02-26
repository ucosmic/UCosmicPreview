using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementContactForm
    {
        public InstitutionalAgreementContactForm()
        {
            //EntityId = Guid.NewGuid();
            //PersonEntityId = Guid.NewGuid();
            Person = new PersonForm();
        }

        //[HiddenInput(DisplayValue = false)]
        //public int RevisionId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public int? AgreementId { get; set; }

        [Display(Name = "Contact type")]
        [Required(ErrorMessage = "{0} is required.")]
        [AllowedContactType(ErrorMessage = "Contact type '{0}' is not allowed. Please select a Contact type from the list provided.")]
        public string Type { get; set; }

        public PersonForm Person { get; set; }
        public class PersonForm
        {
            [HiddenInput(DisplayValue = false)]
            public Guid? EntityId { get; set; }

            [Display(Name = "Salutation")]
            public string Salutation { get; set; }

            [Display(Name = "First name")]
            [Required(ErrorMessage = "Contact first name is required.")]
            public string FirstName { get; set; }

            [Display(Name = "Middle name or initial")]
            public string MiddleName { get; set; }

            [Display(Name = "Last name")]
            [Required(ErrorMessage = "Contact last name is required.")]
            public string LastName { get; set; }

            [Display(Name = "Suffix")]
            public string Suffix { get; set; }

            private string _displayName;
            public string DisplayName
            {
                get
                {
                    return !string.IsNullOrWhiteSpace(_displayName)
                        ? _displayName
                        : string.Format("{0} {1} {2} {3} {4}", Salutation,
                            FirstName, MiddleName, LastName, Suffix);
                }
                set { _displayName = value; }
            }

            [Display(Name = "Email address")]
            [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                ErrorMessage = "This is not a valid email address.")]
            public string DefaultEmail { get; set; }
        }

        //[HiddenInput(DisplayValue = false)]
        //public int PersonId { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public Guid PersonEntityId { get; set; }

        //[Display(Name = "Salutation")]
        //public string PersonSalutation { get; set; }

        //[Display(Name = "First name")]
        //[Required(ErrorMessage = "Contact first name is required.")]
        //public string PersonFirstName { get; set; }

        //[Display(Name = "Middle name or initial")]
        //public string PersonMiddleName { get; set; }

        //[Display(Name = "Last name")]
        //[Required(ErrorMessage = "Contact last name is required.")]
        //public string PersonLastName { get; set; }

        //[Display(Name = "Suffix")]
        //public string PersonSuffix { get; set; }

        //private string _personDisplayName;
        //public string PersonDisplayName
        //{
        //    get
        //    {
        //        return !string.IsNullOrWhiteSpace(_personDisplayName)
        //            ? _personDisplayName
        //            : string.Format("{0} {1} {2} {3} {4}", PersonSalutation,
        //                PersonFirstName, PersonMiddleName, PersonLastName, PersonSuffix);
        //    }
        //    set { _personDisplayName = value; }
        //}

        //[Display(Name = "Email address")]
        //[RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
        //    ErrorMessage = "This is not a valid email address.")]
        //public string PersonDefaultEmail { get; set; }

    }
}