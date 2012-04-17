using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models.EmailAddresses
{
    public class ChangeSpellingForm : IReturnUrl //, IValidatableObject
    {
        public const string ValuePropertyName = "Value";

        [Display(Name = "New spelling")]
        //[Required(ErrorMessage = ChangeEmailSpellingValidationMessage)]
        [Remote("CheckEmailSpelling", "EmailAddresses", "My", HttpMethod = "POST", AdditionalFields = "PersonUserName,Number")]
        public string Value { get; set; }

        [Display(Name = "Current spelling")]
        public string OldSpelling { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string PersonUserName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Number { get; set; }

        //[HiddenInput(DisplayValue = false)]
        //public Guid EntityId { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var queryEntities = DependencyInjector.Current.GetService<IQueryEntities>();
        //    var finder = new PersonFinder(queryEntities);
        //    var person = finder.FindOne(PersonBy.EmailEntityId(EntityId));
        //    if (person != null)
        //    {
        //        var email = person.Emails.Current(EntityId);
        //        if (!email.Value.Equals(Value, StringComparison.OrdinalIgnoreCase))
        //            yield return new ValidationResult(ChangeEmailSpellingValidationMessage);
        //    }
        //}
    }
}