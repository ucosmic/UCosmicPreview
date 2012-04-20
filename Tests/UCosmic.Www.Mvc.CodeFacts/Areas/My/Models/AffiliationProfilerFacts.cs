using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.People;
using System;

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
            public void MapsEmployeeOrStudentAffiliation_ToEmployeeOnly_WhenEstablishmentIsNotInstitution()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                    IsClaimingEmployee = false,
                    IsClaimingStudent = true,
                    Establishment = new Establishment
                    {
                        Type = new EstablishmentType
                        {
                            Category = new EstablishmentCategory
                            {
                                Code = "not an institution"
                            }
                        }
                    }
                };

                var model = Mapper.Map<AffiliationForm>(entity);

                model.EmployeeOrStudentAffiliation.ShouldEqual(EmployeeOrStudentAffiliate.EmployeeOnly);
            }

            [TestMethod]
            public void MapsEmployeeOrStudentAffiliation_ToNull_WhenInstitutionalAffiliationIsNotAcknowledged()
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
                model.EmployeeOrStudentAffiliation.ShouldBeNull();
            }

            [TestMethod]
            public void MapsEmployeeOrStudentAffiliation_ToEmployeeOnly_WhenInstitutionalAffiliationIsAcknowledged_AndIsClaimingEmployee_AndIsNotClaimingStudent()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                    IsClaimingEmployee = true,
                    IsClaimingStudent = false,
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

                model.EmployeeOrStudentAffiliation.ShouldEqual(EmployeeOrStudentAffiliate.EmployeeOnly);
            }

            [TestMethod]
            public void MapsEmployeeOrStudentAffiliation_ToBoth_WhenInstitutionalAffiliationIsAcknowledged_AndIsClaimingEmployee_AndIsClaimingStudent()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                    IsClaimingEmployee = true,
                    IsClaimingStudent = true,
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

                model.EmployeeOrStudentAffiliation.ShouldEqual(EmployeeOrStudentAffiliate.Both);
            }

            [TestMethod]
            public void MapsEmployeeOrStudentAffiliation_ToNeither_WhenInstitutionalAffiliationIsAcknowledged_AndIsNotClaimingEmployee_AndIsNotClaimingStudent()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                    IsClaimingEmployee = false,
                    IsClaimingStudent = false,
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

                model.EmployeeOrStudentAffiliation.ShouldEqual(EmployeeOrStudentAffiliate.Neither);
            }

            [TestMethod]
            public void MapsEmployeeOrStudentAffiliation_ToStudentOnly_WhenInstitutionalAffiliationIsAcknowledged_AndIsNotClaimingEmployee_AndIsClaimingStudent()
            {
                var entity = new Affiliation
                {
                    IsAcknowledged = true,
                    IsClaimingEmployee = false,
                    IsClaimingStudent = true,
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

                model.EmployeeOrStudentAffiliation.ShouldEqual(EmployeeOrStudentAffiliate.StudentOnly);
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
            public void MapsIsClaimingStudent_ToFalse_WhenEmployeeOrStudentAffiliation_IsNeither()
            {
                var model = new AffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.Neither };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingStudent.ShouldBeFalse();
            }

            [TestMethod]
            public void MapsIsClaimingStudent_ToFalse_WhenEmployeeOrStudentAffiliation_IsEmployeeOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.EmployeeOnly };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingStudent.ShouldBeFalse();
            }

            [TestMethod]
            public void MapsIsClaimingStudent_ToTrue_WhenEmployeeOrStudentAffiliation_IsStudentOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.StudentOnly };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingStudent.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsIsClaimingStudent_ToTrue_WhenEmployeeOrStudentAffiliation_IsBoth()
            {
                var model = new AffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.Both };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingStudent.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsIsClaimingEmployee_ToFalse_WhenEmployeeOrStudentAffiliation_IsNeither()
            {
                var model = new AffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.Neither };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingEmployee.ShouldBeFalse();
            }

            [TestMethod]
            public void MapsIsClaimingEmployee_ToTrue_WhenEmployeeOrStudentAffiliation_IsEmployeeOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.EmployeeOnly };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingEmployee.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsIsClaimingEmployee_ToFalse_WhenEmployeeOrStudentAffiliation_IsStudentOnly()
            {
                var model = new AffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.StudentOnly };

                var command = Mapper.Map<UpdateMyAffiliationCommand>(model);

                command.ShouldNotBeNull();
                command.IsClaimingEmployee.ShouldBeFalse();
            }

            [TestMethod]
            public void MapsIsClaimingEmployee_ToTrue_WhenEmployeeOrStudentAffiliation_IsBoth()
            {
                var model = new AffiliationForm { EmployeeOrStudentAffiliation = EmployeeOrStudentAffiliate.Both };

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
