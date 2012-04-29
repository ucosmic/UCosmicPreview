using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Models.SignUp
{
    // ReSharper disable UnusedMember.Global
    public class CreatePasswordFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class Password
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenNull()
            {
                // arrange
                var model = new CreatePasswordForm { Password = null, };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateProperty(model.Password,
                    new ValidationContext(model, null, null) { MemberName = "Password" }, results);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(CreatePasswordForm.PasswordRequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("Password");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenEmptyString()
            {
                // arrange
                var model = new CreatePasswordForm { Password = string.Empty };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateProperty(model.Password,
                    new ValidationContext(model, null, null) { MemberName = "Password" }, results);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(CreatePasswordForm.PasswordRequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("Password");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenWhiteSpaceString()
            {
                // arrange
                var model = new CreatePasswordForm { Password = "  \r\n \t  " };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateProperty(model.Password,
                    new ValidationContext(model, null, null) { MemberName = "Password" }, results);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(CreatePasswordForm.PasswordRequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("Password");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenTooShort()
            {
                // arrange
                var model = new CreatePasswordForm { Password = "short" };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateProperty(model.Password,
                    new ValidationContext(model, null, null) { MemberName = "Password" }, results);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(string.Format(
                    CreatePasswordForm.PasswordLengthErrorMessage, null, 100, 6));
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("Password");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenHasTooLong()
            {
                // arrange
                var model = new CreatePasswordForm { Password = "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789x" };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateProperty(model.Password,
                    new ValidationContext(model, null, null) { MemberName = "Password" }, results);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(string.Format(
                    CreatePasswordForm.PasswordLengthErrorMessage, null, 100, 6));
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("Password");
            }

        }

        [TestClass]
        public class ConfirmPassword
        {
            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenNull()
            {
                // arrange
                var model = new CreatePasswordForm { ConfirmPassword = null, };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateProperty(model.ConfirmPassword,
                    new ValidationContext(model, null, null) { MemberName = "ConfirmPassword" }, results);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(CreatePasswordForm.ConfirmationRequiredErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(1);
                results.Single().MemberNames.Single().ShouldEqual("ConfirmPassword");
            }

            [TestMethod, TestCategory("Identity"), TestCategory("SignUp")]
            public void IsInvalid_WhenDoesNotMatchPassword()
            {
                // arrange
                var model = new CreatePasswordForm { Password = "password", ConfirmPassword = "pass word" };
                var results = new List<ValidationResult>();

                // act
                var isValid = Validator.TryValidateProperty(model.ConfirmPassword,
                    new ValidationContext(model, null, null) { MemberName = "ConfirmPassword" }, results);

                // assert
                isValid.ShouldBeFalse();
                results.Count.ShouldEqual(1);
                results.Single().ErrorMessage.ShouldNotBeNull();
                results.Single().ErrorMessage.ShouldEqual(CreatePasswordForm.PasswordCompareErrorMessage);
                results.Single().MemberNames.ShouldNotBeNull();
                results.Single().MemberNames.Count().ShouldEqual(0);
            }

        }
    }
}
