//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Globalization;
//using System.Linq;
//using System.Web.Mvc;
//using UCosmic.Domain;
//using UCosmic.Domain.People;

//namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignUp
//{
//    public class OldConfirmEmailForm : IValidatableObject
//    {
//        public const string RequiredErrorMessage = "Please enter a confirmation code.";
//        public const string InvalidSecretCodeErrorMessage = "Invalid confirmation code, please try again.";

//        [HiddenInput(DisplayValue = false)]
//        public bool IsUrlConfirmation { get; set; }

//        [HiddenInput(DisplayValue = false)]
//        public Guid Token { get; set; }

//        [Display(Name = "Confirmation Code", Prompt = "Copy & paste your secret Confirmation Code here")]
//        [DataType(DataType.Password)]
//        [Required(ErrorMessage = RequiredErrorMessage)]
//        [Remote("ValidateConfirmEmail", "OldSignUp", "Identity", AdditionalFields = "Token",
//            HttpMethod = "POST", ErrorMessage = InvalidSecretCodeErrorMessage)]
//        public string SecretCode { get; set; }

//        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
//        {
//            if (!string.IsNullOrWhiteSpace(SecretCode))
//            {
//                // make sure the secret code is valid
//                var queryEntities = DependencyInjector.Current.GetService<IQueryEntities>();
//                var finder = new PersonFinder(queryEntities);
//                var person = finder.FindOne(PersonBy.EmailConfirmation(Token, EmailConfirmationIntent.SignUp));
//                var confirmation = (person != null)
//                    ? person.Emails.SelectManyConfirmations()
//                        .SingleOrDefault(c => c.Token == Token && c.Intent == EmailConfirmationIntent.SignUp
//                            && c.SecretCode == SecretCode)
//                    : null;
//                if (confirmation == null)
//                    return new List<ValidationResult>
//                        {
//                            new ValidationResult(string.Format(CultureInfo.CurrentCulture, 
//                                InvalidSecretCodeErrorMessage), new [] { "SecretCode" })
//                        };
//            }
//            return new List<ValidationResult>();
//        }
//    }
//}