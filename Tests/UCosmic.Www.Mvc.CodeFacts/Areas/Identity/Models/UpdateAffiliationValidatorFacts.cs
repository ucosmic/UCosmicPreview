using System.Linq;
using System.Web;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class UpdateAffiliationValidatorFacts
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Validates_UpdateAffiliationForm()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));
                var container = SimpleDependencyInjector.Bootstrap(new ContainerConfiguration());

                var validator = container.GetInstance<IValidator<UpdateAffiliationForm>>();

                validator.ShouldNotBeNull();
                validator.ShouldBeType<UpdateAffiliationValidator>();
            }
        }

        [TestClass]
        public class TheEmployeeOrStudentProperty
        {
            [TestMethod]
            public void IsInvalidWhen_EmployeeOrStudentAffiliation_IsNull()
            {
                var validator = new UpdateAffiliationValidator();
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = null };
                var results = validator.Validate(model);
                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e =>
                    e.PropertyName == UpdateAffiliationForm.EmployeeOrStudentAffiliationPropertyName);
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(
                    UpdateAffiliationValidator.FailedBecauseEmployeeOrStudentAffiliationWasEmpty);
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_EmployeeOrStudentAffiliation_IsNotEmpty()
            {
                var validator = new UpdateAffiliationValidator();
                var model = new UpdateAffiliationForm
                {
                    EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.Neither
                };
                var results = validator.Validate(model);
                var error = results.Errors.SingleOrDefault(e =>
                    e.PropertyName == UpdateAffiliationForm.EmployeeOrStudentAffiliationPropertyName);
                error.ShouldBeNull();
            }
        }
    }
}
