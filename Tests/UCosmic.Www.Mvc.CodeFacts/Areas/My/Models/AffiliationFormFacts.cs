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
    public class AffiliationFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Implements_IReturnUrl()
            {
                var model = new AffiliationForm();
                model.ShouldImplement<IReturnUrl>();
            }
        }

        [TestClass]
        public class TheReturnUrlProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<AffiliationForm, string>> property = p => p.ReturnUrl;
                var attributes = property.GetAttributes<AffiliationForm, string, HiddenInputAttribute>();
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
                Expression<Func<AffiliationForm, string>> property = p => p.JobTitles;
                var attributes = property.GetAttributes<AffiliationForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(AffiliationForm.JobTitlesDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_DisplayFormat_UsingNullDisplayText()
            {
                Expression<Func<AffiliationForm, string>> property = p => p.JobTitles;
                var attributes = property.GetAttributes<AffiliationForm, string, DisplayFormatAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].NullDisplayText.ShouldEqual(AffiliationForm.JobTitlesNullDisplayText);
            }

            [TestMethod]
            public void IsDecoratedWith_DataType_UsingMultilineText()
            {
                Expression<Func<AffiliationForm, string>> property = p => p.JobTitles;
                var attributes = property.GetAttributes<AffiliationForm, string, DataTypeAttribute>();
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
                Expression<Func<AffiliationForm, int>> property = p => p.EstablishmentId;
                var attributes = property.GetAttributes<AffiliationForm, int, HiddenInputAttribute>();
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
                Expression<Func<AffiliationForm, bool>> property = p => p.IsAcknowledged;
                var attributes = property.GetAttributes<AffiliationForm, bool, HiddenInputAttribute>();
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
                Expression<Func<AffiliationForm, bool>> property = p => p.IsClaimingInternationalOffice;
                var attributes = property.GetAttributes<AffiliationForm, bool, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(AffiliationForm.IsClaimingInternationalOfficeDisplayName);
            }
        }

        [TestClass]
        public class TheIsClaimingEmployeeProperty
        {
            [TestMethod]
            public void IsTrue_WhenEmployeeOrStudent_IsBoth()
            {
                var model = new AffiliationForm {EmployeeOrStudent = EmployeeOrStudentAnswer.Both};
                model.IsClaimingEmployee.ShouldBeTrue();
            }

            [TestMethod]
            public void IsTrue_WhenEmployeeOrStudent_IsEmployeeOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.EmployeeOnly };
                model.IsClaimingEmployee.ShouldBeTrue();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudent_IsStudentOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.StudentOnly };
                model.IsClaimingEmployee.ShouldBeFalse();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudent_IsNeither()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.Neither };
                model.IsClaimingEmployee.ShouldBeFalse();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudent_IsNull()
            {
                var model = new AffiliationForm { EmployeeOrStudent = null };
                model.IsClaimingEmployee.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheIsClaimingStudentProperty
        {
            [TestMethod]
            public void IsTrue_WhenEmployeeOrStudent_IsBoth()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.Both };
                model.IsClaimingStudent.ShouldBeTrue();
            }

            [TestMethod]
            public void IsTrue_WhenEmployeeOrStudent_IsEmployeeOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.StudentOnly };
                model.IsClaimingStudent.ShouldBeTrue();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudent_IsStudentOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.EmployeeOnly };
                model.IsClaimingStudent.ShouldBeFalse();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudent_IsNeither()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.Neither };
                model.IsClaimingStudent.ShouldBeFalse();
            }

            [TestMethod]
            public void IsFalse_WhenEmployeeOrStudent_IsNull()
            {
                var model = new AffiliationForm { EmployeeOrStudent = null };
                model.IsClaimingStudent.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheIsClaimingAdministratorProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<AffiliationForm, bool>> property = p => p.IsClaimingAdministrator;
                var attributes = property.GetAttributes<AffiliationForm, bool, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(AffiliationForm.IsClaimingAdministratorDisplayName);
            }
        }

        [TestClass]
        public class TheIsClaimingFacultyProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<AffiliationForm, bool>> property = p => p.IsClaimingFaculty;
                var attributes = property.GetAttributes<AffiliationForm, bool, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(AffiliationForm.IsClaimingFacultyDisplayName);
            }
        }

        [TestClass]
        public class TheIsClaimingStaffProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<AffiliationForm, bool>> property = p => p.IsClaimingStaff;
                var attributes = property.GetAttributes<AffiliationForm, bool, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(AffiliationForm.IsClaimingStaffDisplayName);
            }
        }

        [TestClass]
        public class TheEstablishmentInfoClass_OfficialNameProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<AffiliationForm.EstablishmentInfo, string>> property = p => p.OfficialName;
                var attributes = property.GetAttributes<AffiliationForm.EstablishmentInfo, string, HiddenInputAttribute>();
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
                Expression<Func<AffiliationForm.EstablishmentInfo, bool>> property = p => p.IsInstitution;
                var attributes = property.GetAttributes<AffiliationForm.EstablishmentInfo, bool, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }
    }
}
