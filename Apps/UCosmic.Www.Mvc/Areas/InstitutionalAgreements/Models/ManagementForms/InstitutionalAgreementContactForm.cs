using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Www.Mvc.Models;
using System.Web;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Models.ManagementForms
{
    public class InstitutionalAgreementContactForm
    {
        public InstitutionalAgreementContactForm()
        {
            Person = new PersonForm();
        }

        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid AgreementEntityId { get; set; }

        [Display(Name = ContactTypeDisplayName)]
        [Required(ErrorMessage = ContactTypeRequiredErrorFormat)]
        [AllowedContactType(ErrorMessage = "Contact type '{0}' is not allowed. Please select a Contact type from the list provided.")]
        public string ContactType { get; set; }
        public const string ContactTypeDisplayName = "Contact type";
        public const string ContactTypeRequiredErrorFormat = "{0} is required.";

        public PersonForm Person { get; set; }
        public class PersonForm
        {
            [HiddenInput(DisplayValue = false)]
            public Guid? EntityId { get; set; }

            [Display(Name = "Salutation")]
            public string Salutation { get; set; }

            [Display(Name = FirstNameDisplayName)]
            [Required(ErrorMessage = FirstNameRequiredErrorText)]
            public string FirstName { get; set; }
            public const string FirstNameDisplayName = "First name";
            public const string FirstNameRequiredErrorText = "Contact first name is required.";

            [Display(Name = "Middle name or initial")]
            public string MiddleName { get; set; }

            [Display(Name = LastNameDisplayName)]
            [Required(ErrorMessage = LastNameRequiredErrorText)]
            public string LastName { get; set; }
            public const string LastNameDisplayName = "Last name";
            public const string LastNameRequiredErrorText = "Contact last name is required.";

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
    }

    public static class InstitutionalAgreementContactProfiler
    {
        public static void RegisterProfiles()
        {
            RootModelProfiler.RegisterProfiles(typeof(InstitutionalAgreementContactProfiler));
        }

        internal class ModelToCommandProfile : Profile
        {
            protected override void Configure()
            {
                CreateMap<InstitutionalAgreementContactForm, AddContactToAgreementCommand>()
                    .ConstructUsing((Func<ResolutionContext, AddContactToAgreementCommand>)
                        (r => new AddContactToAgreementCommand(HttpContext.Current.User)))
                    .ForMember(d => d.Principal, o => o.Ignore())
                    .ForMember(d => d.IsNewlyAdded, o => o.Ignore())
                ;
            }
        }
    }
}