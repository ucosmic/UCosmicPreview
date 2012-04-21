using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    // ReSharper disable UnusedMember.Global
    public class UpdateAffiliationFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Implements_IReturnUrl()
            {
                var model = new UpdateAffiliationForm();
                model.ShouldImplement<IReturnUrl>();
            }
        }

        [TestClass]
        public class TheReturnUrlProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<UpdateAffiliationForm, string>> property = p => p.ReturnUrl;
                var attributes = property.GetAttributes<UpdateAffiliationForm, string, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheJobTitlesProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateAffiliationForm, string>> property = p => p.JobTitles;
                var attributes = property.GetAttributes<UpdateAffiliationForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateAffiliationForm.JobTitlesDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_DisplayFormat_UsingNullDisplayText()
            {
                Expression<Func<UpdateAffiliationForm, string>> property = p => p.JobTitles;
                var attributes = property.GetAttributes<UpdateAffiliationForm, string, DisplayFormatAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].NullDisplayText.ShouldEqual(UpdateAffiliationForm.JobTitlesNullDisplayText);
            }

            [TestMethod]
            public void IsDecoratedWith_DataType_UsingMultilineText()
            {
                Expression<Func<UpdateAffiliationForm, string>> property = p => p.JobTitles;
                var attributes = property.GetAttributes<UpdateAffiliationForm, string, DataTypeAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DataType.ShouldEqual(DataType.MultilineText);
            }
        }

        [TestClass]
        public class TheEstablishmentIdProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<UpdateAffiliationForm, int>> property = p => p.EstablishmentId;
                var attributes = property.GetAttributes<UpdateAffiliationForm, int, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheIsAcknowledgedProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<UpdateAffiliationForm, bool>> property = p => p.IsAcknowledged;
                var attributes = property.GetAttributes<UpdateAffiliationForm, bool, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheIsClaimingInternationalOfficeProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateAffiliationForm, bool>> property = p => p.IsClaimingInternationalOffice;
                var attributes = property.GetAttributes<UpdateAffiliationForm, bool, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateAffiliationForm.IsClaimingInternationalOfficeDisplayName);
            }
        }

        [TestClass]
        public class TheIsClaimingEmployeeProperty
        {
            [TestMethod]
            public void IsTrue_WhenEmployeeOrStudentAffiliation_IsBoth()
            {
                var model = new UpdateAffiliationForm {EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.Both};
                model.IsClaimingEmployee.ShouldBeTrue();
            }

            [TestMethod]
            public void IsTrue_WhenEmployeeOrStudentAffiliation_IsEmployeeOnly()
            {
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.EmployeeOnly };
                model.IsClaimingEmployee.ShouldBeTrue();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudentAffiliation_IsStudentOnly()
            {
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.StudentOnly };
                model.IsClaimingEmployee.ShouldBeFalse();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudentAffiliation_IsNeither()
            {
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.Neither };
                model.IsClaimingEmployee.ShouldBeFalse();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudentAffiliation_IsNull()
            {
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = null };
                model.IsClaimingEmployee.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheIsClaimingStudentProperty
        {
            [TestMethod]
            public void IsTrue_WhenEmployeeOrStudentAffiliation_IsBoth()
            {
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.Both };
                model.IsClaimingStudent.ShouldBeTrue();
            }

            [TestMethod]
            public void IsTrue_WhenEmployeeOrStudentAffiliation_IsEmployeeOnly()
            {
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.StudentOnly };
                model.IsClaimingStudent.ShouldBeTrue();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudentAffiliation_IsStudentOnly()
            {
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.EmployeeOnly };
                model.IsClaimingStudent.ShouldBeFalse();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudentAffiliation_IsNeither()
            {
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.Neither };
                model.IsClaimingStudent.ShouldBeFalse();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudentAffiliation_IsNull()
            {
                var model = new UpdateAffiliationForm { EmployeeOrStudentAffiliation = null };
                model.IsClaimingStudent.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheIsClaimingAdministratorProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateAffiliationForm, bool>> property = p => p.IsClaimingAdministrator;
                var attributes = property.GetAttributes<UpdateAffiliationForm, bool, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateAffiliationForm.IsClaimingAdministratorDisplayName);
            }
        }

        [TestClass]
        public class TheIsClaimingFacultyProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateAffiliationForm, bool>> property = p => p.IsClaimingFaculty;
                var attributes = property.GetAttributes<UpdateAffiliationForm, bool, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateAffiliationForm.IsClaimingFacultyDisplayName);
            }
        }

        [TestClass]
        public class TheIsClaimingStaffProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateAffiliationForm, bool>> property = p => p.IsClaimingStaff;
                var attributes = property.GetAttributes<UpdateAffiliationForm, bool, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateAffiliationForm.IsClaimingStaffDisplayName);
            }
        }

        [TestClass]
        public class TheEstablishmentInfoClass_OfficialNameProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<UpdateAffiliationForm.EstablishmentInfo, string>> property = p => p.OfficialName;
                var attributes = property.GetAttributes<UpdateAffiliationForm.EstablishmentInfo, string, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheEstablishmentInfoClass_IsInstitutionProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<UpdateAffiliationForm.EstablishmentInfo, bool>> property = p => p.IsInstitution;
                var attributes = property.GetAttributes<UpdateAffiliationForm.EstablishmentInfo, bool, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }
    }
}
