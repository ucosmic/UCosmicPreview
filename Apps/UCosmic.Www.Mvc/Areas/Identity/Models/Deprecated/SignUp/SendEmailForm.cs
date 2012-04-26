using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignUp
{
    public class SendEmailForm : IValidatableObject
    {
        public const string RequiredErrorMessage = "Email Address is required.";
        public const string LengthErrorMessage = "Email address cannot contain more than {1} characters.";
        public const string RegexErrorMessage = "This is not a valid email address.";
        public const string IneligibleErrorMessage = "Sorry, emails ending in '{0}' are not eligible at this time.";
        public const string AlreadySignedUpErrorMessage = "The email '{0}' has already been signed up.";

        [Display(Name = "Email Address", Prompt = "Enter your work email address here")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(200, ErrorMessage = LengthErrorMessage)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = RegexErrorMessage)]
        [Remote("ValidateSendEmail", "SignUp", "Identity", HttpMethod = "POST")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            const char at = '@';
            if (!string.IsNullOrWhiteSpace(EmailAddress) && EmailAddress.Contains(at))
            {
                // find out whether the email domain is an eligible member
                var emailDomain = EmailAddress.Substring(EmailAddress.LastIndexOf(at));
                var establishments = new EstablishmentFinder(DependencyInjector.Current.GetService<IQueryEntities>());

                var establishment = establishments.FindOne(EstablishmentBy.EmailDomain(EmailAddress));
                if (establishment == null || !establishment.IsMember)
                    return new List<ValidationResult>
                        {
                            new ValidationResult(string.Format(CultureInfo.CurrentCulture, 
                                IneligibleErrorMessage, emailDomain), new [] { "EmailAddress" })
                        };

                // make sure email is not already signed up
                var queryEntities = DependencyInjector.Current.GetService<IQueryEntities>();
                var finder = new PersonFinder(queryEntities);
                var person = finder.FindOne(PersonBy.EmailAddress(EmailAddress)
                    .EagerLoad(p => p.User)
                );
                if (person != null && person.User != null && person.User.IsRegistered)
                    return new List<ValidationResult>
                        {
                            new ValidationResult(string.Format(CultureInfo.CurrentCulture, 
                                AlreadySignedUpErrorMessage, EmailAddress), new [] { "EmailAddress" })
                        };

                // make sure there is not already a member with this email address
                var memberSigner = DependencyInjector.Current.GetService<ISignMembers>();
                var isSignedUp = memberSigner.IsSignedUp(EmailAddress);
                if (isSignedUp)
                    return new List<ValidationResult>
                        {
                            new ValidationResult(string.Format(CultureInfo.CurrentCulture, 
                                AlreadySignedUpErrorMessage, EmailAddress), new [] { "EmailAddress" })
                        };
            }
            return new List<ValidationResult>();
        }
    }


}