﻿using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    // ReSharper disable UnusedMember.Global
    public class AffiliationProfilerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheEntityToViewModelProfile
        {
            [TestMethod]
            public void MapsEmployeeOrStudent_ToNull_WhenAffiliationIsNotAcknowledged()
            {
                var entity = new Affiliation();

                var model = Mapper.Map<AffiliationForm>(entity);

                model.EmployeeOrStudent.ShouldBeNull();
            }

            [TestMethod]
            public void MapsEmployeeOrStudent_ToEmployeeOnly_WhenAffiliationIsAcknowledged_AndIsClaimingEmployee_AndIsNotClaimingStudent()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                    IsClaimingEmployee = true,
                    IsClaimingStudent = false,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.EmployeeOrStudent.ShouldEqual(EmployeeOrStudentAnswer.EmployeeOnly);
            }

            [TestMethod]
            public void MapsEmployeeOrStudent_ToBoth_WhenAffiliationIsAcknowledged_AndIsClaimingEmployee_AndIsClaimingStudent()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                    IsClaimingEmployee = true,
                    IsClaimingStudent = true,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.EmployeeOrStudent.ShouldEqual(EmployeeOrStudentAnswer.Both);
            }

            [TestMethod]
            public void MapsEmployeeOrStudent_ToNeither_WhenAffiliationIsAcknowledged_AndIsNotClaimingEmployee_AndIsNotClaimingStudent()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                    IsClaimingEmployee = false,
                    IsClaimingStudent = false,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.EmployeeOrStudent.ShouldEqual(EmployeeOrStudentAnswer.Neither);
            }

            [TestMethod]
            public void MapsEmployeeOrStudent_ToStudentOnly_WhenAffiliationIsAcknowledged_AndIsNotClaimingEmployee_AndIsClaimingStudent()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                    IsClaimingEmployee = false,
                    IsClaimingStudent = true,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.EmployeeOrStudent.ShouldEqual(EmployeeOrStudentAnswer.StudentOnly);
            }

            [TestMethod]
            public void IgnoresReturnUrl()
            {
                var entity = new Affiliation();

                var model = Mapper.Map<AffiliationForm>(entity);

                model.ShouldNotBeNull();
                model.ReturnUrl.ShouldBeNull();
            }

            [TestMethod]
            public void MapsJobTitles()
            {
                var entity = new Affiliation
                {
                    JobTitles = "test",
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.JobTitles.ShouldEqual(entity.JobTitles);
            }

            [TestMethod]
            public void MapsEstablishmentId()
            {
                var entity = new Affiliation
                {
                    EstablishmentId = 92,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.EstablishmentId.ShouldEqual(entity.EstablishmentId);
            }

            [TestMethod]
            public void MapsEstablishmentInfo_OfficialName()
            {
                var entity = new Affiliation
                {
                    Establishment = new Establishment
                    {
                        OfficialName = "test",
                        Type = new EstablishmentType
                        {
                            Category = new EstablishmentCategory
                            {
                                Code = "not a real value",
                            },
                        },
                    },
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.Establishment.OfficialName.ShouldEqual(entity.Establishment.OfficialName);
            }

            [TestMethod]
            public void MapsEstablishmentInfo_IsInsitution()
            {
                var entity = new Affiliation
                {
                    Establishment = new Establishment
                    {
                        Type = new EstablishmentType
                        {
                            Category = new EstablishmentCategory
                            {
                                Code = EstablishmentCategoryCode.Inst,
                            },
                        },
                    },
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.Establishment.IsInstitution.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsIsAcknowledged()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.IsAcknowledged.ShouldEqual(entity.IsAcknowledged);
            }

            //[TestMethod]
            //public void MapsIsClaimingEmployee()
            //{
            //    var entity = new Affiliation
            //    {
            //        IsClaimingEmployee = true,
            //    };

            //    var model = Mapper.Map<AffiliationForm>(entity);

            //    model.IsClaimingEmployee.ShouldEqual(entity.IsClaimingEmployee);
            //}

            [TestMethod]
            public void MapsIsClaimingInternationalOffice()
            {
                var entity = new Affiliation
                {
                    IsClaimingInternationalOffice = true,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.IsClaimingInternationalOffice.ShouldEqual(entity.IsClaimingInternationalOffice);
            }

            [TestMethod]
            public void MapsIsClaimingAdministrator()
            {
                var entity = new Affiliation
                {
                    IsClaimingAdministrator = true,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.IsClaimingAdministrator.ShouldEqual(entity.IsClaimingAdministrator);
            }

            [TestMethod]
            public void MapsIsClaimingFaculty()
            {
                var entity = new Affiliation
                {
                    IsClaimingFaculty = true,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.IsClaimingFaculty.ShouldEqual(entity.IsClaimingFaculty);
            }

            [TestMethod]
            public void MapsIsClaimingStaff()
            {
                var entity = new Affiliation
                {
                    IsClaimingStaff = true,
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.IsClaimingStaff.ShouldEqual(entity.IsClaimingStaff);
            }
        }

        [TestClass]
        public class TheViewModelToCommandProfile
        {
            [TestMethod]
            public void MapsIsClaimingStudent_ToFalse_WhenEmployeeOrStudent_IsNeither()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.Neither };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingStudent.ShouldBeFalse();
            }

            [TestMethod]
            public void MapsIsClaimingStudent_ToFalse_WhenEmployeeOrStudent_IsEmployeeOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.EmployeeOnly };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingStudent.ShouldBeFalse();
            }

            [TestMethod]
            public void MapsIsClaimingStudent_ToTrue_WhenEmployeeOrStudent_IsStudentOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.StudentOnly };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingStudent.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsIsClaimingStudent_ToTrue_WhenEmployeeOrStudent_IsBoth()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.Both };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingStudent.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsIsClaimingEmployee_ToFalse_WhenEmployeeOrStudent_IsNeither()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.Neither };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingEmployee.ShouldBeFalse();
            }

            [TestMethod]
            public void MapsIsClaimingEmployee_ToTrue_WhenEmployeeOrStudent_IsEmployeeOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.EmployeeOnly };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingEmployee.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsIsClaimingEmployee_ToFalse_WhenEmployeeOrStudent_IsStudentOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.StudentOnly };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingEmployee.ShouldBeFalse();
            }

            [TestMethod]
            public void MapsIsClaimingEmployee_ToTrue_WhenEmployeeOrStudent_IsBoth()
            {
                var model = new AffiliationForm { EmployeeOrStudent = EmployeeOrStudentAnswer.Both };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingEmployee.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsEstablishmentId()
            {
                var model = new AffiliationForm { EstablishmentId = 96 };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.EstablishmentId.ShouldEqual(model.EstablishmentId);
            }

            [TestMethod]
            public void MapsJobTitles()
            {
                var model = new AffiliationForm { JobTitles = "test" };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.JobTitles.ShouldEqual(model.JobTitles);
            }

            [TestMethod]
            public void MapsIsClaimingInternationalOffice()
            {
                var model = new AffiliationForm { IsClaimingInternationalOffice = true };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingInternationalOffice.ShouldEqual(model.IsClaimingInternationalOffice);
            }

            [TestMethod]
            public void MapsIsClaimingAdministrator()
            {
                var model = new AffiliationForm { IsClaimingAdministrator = true };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingAdministrator.ShouldEqual(model.IsClaimingAdministrator);
            }

            [TestMethod]
            public void MapsIsClaimingFaculty()
            {
                var model = new AffiliationForm { IsClaimingFaculty = true };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingFaculty.ShouldEqual(model.IsClaimingFaculty);
            }

            [TestMethod]
            public void MapsIsClaimingStaff()
            {
                var model = new AffiliationForm { IsClaimingStaff = true };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingStaff.ShouldEqual(model.IsClaimingStaff);
            }

            [TestMethod]
            public void IgnoresChangeCount()
            {
                var model = new AffiliationForm();

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.ChangeCount.ShouldEqual(0);
            }
        }
    }
}
