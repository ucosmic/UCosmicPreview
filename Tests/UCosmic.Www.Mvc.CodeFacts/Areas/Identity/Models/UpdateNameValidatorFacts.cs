using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class UpdateNameValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_UpdateNameForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap(new ContainerConfiguration());

                var validator = container.GetInstance<IValidator<UpdateNameForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<UpdateNameValidator>();
            }
        }

        [TestClass]
        public class TheDisplayNameProperty
        {
            private const string PropertyName = "DisplayName";

            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsNull()
            {
                var validator = new UpdateNameValidator();
                var model = new UpdateNameForm { DisplayName = null };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateNameValidator.FailedBecauseDisplayNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsEmpty()
            {
                var validator = new UpdateNameValidator();
                var model = new UpdateNameForm { DisplayName = string.Empty };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateNameValidator.FailedBecauseDisplayNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_DisplayName_IsWhiteSpace()
            {
                var validator = new UpdateNameValidator();
                var model = new UpdateNameForm { DisplayName = " \r" };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateNameValidator.FailedBecauseDisplayNameWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_DisplayName_IsNotEmpty()
            {
                var validator = new UpdateNameValidator();
                var model = new UpdateNameForm { DisplayName = "test" };
                var results = validator.Validate(model);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == PropertyName);
                error.ShouldBeNull();
            }
        }
    }
}
