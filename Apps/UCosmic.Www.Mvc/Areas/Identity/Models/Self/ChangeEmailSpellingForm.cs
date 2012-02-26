using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Domain;
using UCosmic.Domain.People;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.Self
{
    public class ChangeEmailSpellingForm : IValidatableObject, IReturnUrl
    {
        private const string ChangeEmailSpellingValidationMessage
            = "You can only change lowercase letters to uppercase (or vice versa) when changing the spelling of your email address.";

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [Display(Name = "Current spelling")]
        [ReadOnly(true)]
        public string OldSpelling { get; set; }

        [Display(Name = "New spelling")]
        [Required(ErrorMessage = ChangeEmailSpellingValidationMessage)]
        [Remote("CheckEmailSpelling", "Self", "Identity", AdditionalFields = "EntityId", HttpMethod = "POST")]
        public string Value { get; set; }

        [HiddenInput(DisplayValue = false)]
        public Guid EntityId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var queryEntities = DependencyInjector.Current.GetService<IQueryEntities>();
            var finder = new PersonFinder(queryEntities);
            var person = finder.FindOne(PersonBy.EmailEntityId(EntityId));
            if (person != null)
            {
                var email = person.Emails.Current(EntityId);
                if (!email.Value.Equals(Value, StringComparison.OrdinalIgnoreCase))
                    yield return new ValidationResult(ChangeEmailSpellingValidationMessage);
            }
        }
    }
}