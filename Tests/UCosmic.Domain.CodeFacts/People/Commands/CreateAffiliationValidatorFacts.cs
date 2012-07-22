using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class CreateAffiliationValidatorFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheEstablishmentIdProperty
        {
            [TestMethod]
            public void IsInvalidWhen_DoesNotMatchEstablishment()
            {
                const int establishmentId = 6;
                var command = new CreateAffiliationCommand { EstablishmentId = establishmentId };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                entities.Setup(m => m.Read<Person>()).Returns(new Person[] { }.AsQueryable);
                var validator = new CreateAffiliationValidator(entities.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EstablishmentId");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateEstablishment.FailedBecauseIdMatchedNoEntity,
                        establishmentId));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_MatchesEstablishment_WithAnyTypeCategory()
            {
                const int establishmentId = 6;
                var command = new CreateAffiliationCommand { EstablishmentId = establishmentId };
                var establishment = new Establishment
                {
                    RevisionId = command.EstablishmentId,
                    Type = new EstablishmentType
                    {
                        Category = new EstablishmentCategory
                        {
                            Code = EstablishmentCategoryCode.Govt,
                        },
                    },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                entities.Setup(m => m.Read<Person>()).Returns(new Person[] { }.AsQueryable);
                var validator = new CreateAffiliationValidator(entities.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EstablishmentId");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheIsClaimingStudentProperty
        {
            [TestMethod]
            public void IsInvalidWhen_IsTrue_ButEstablishmentIsNotInstitution()
            {
                const bool isClaimingStudent = true;
                var establishment = new Establishment
                {
                    Type = new EstablishmentType
                    {
                        Category = new EstablishmentCategory
                        {
                            Code = "not an institution",
                        },
                    },
                };
                var command = new CreateAffiliationCommand
                {
                    IsClaimingStudent = isClaimingStudent
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                entities.Setup(m => m.Read<Person>()).Returns(new Person[] { }.AsQueryable);
                var validator = new CreateAffiliationValidator(entities.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "IsClaimingStudent");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidateAffiliation.FailedBecauseIsClaimingStudentButEstablishmentIsNotInstitution,
                        command.EstablishmentId));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void ValidWhen_IsTrue_AndEstablishmentIsInstitution()
            {
                const bool isClaimingStudent = true;
                var command = new CreateAffiliationCommand
                {
                    IsClaimingStudent = isClaimingStudent
                };
                var establishment = new Establishment
                {
                    Type = new EstablishmentType
                    {
                        Category = new EstablishmentCategory
                        {
                            Code = EstablishmentCategoryCode.Inst,
                        },
                    },
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new[] { establishment }.AsQueryable);
                entities.Setup(m => m.Read<Person>()).Returns(new Person[] { }.AsQueryable);
                var validator = new CreateAffiliationValidator(entities.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "IsClaimingStudent");
                error.ShouldBeNull();
            }

            [TestMethod]
            public void ValidWhen_IsTrue_AndEstablishmentWasNull()
            {
                const bool isClaimingStudent = true;
                var command = new CreateAffiliationCommand
                {
                    IsClaimingStudent = isClaimingStudent
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                entities.Setup(m => m.Read<Person>()).Returns(new Person[] { }.AsQueryable);
                var validator = new CreateAffiliationValidator(entities.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "IsClaimingStudent");
                error.ShouldBeNull();
            }
        }

        [TestClass]
        public class ThePersonIdProperty
        {
            [TestMethod]
            public void IsInvalidWhen_PersonId_DoesNotMatchPerson()
            {
                const int personId = 3;
                var command = new CreateAffiliationCommand { PersonId = personId };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                entities.Setup(m => m.Read<Person>()).Returns(new Person[] { }.AsQueryable);
                var validator = new CreateAffiliationValidator(entities.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PersonId");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidatePerson.FailedBecauseIdMatchedNoEntity,
                        personId));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsInvalidWhen_PersonId_IsAlreadyAffiliatedWithEstablishment()
            {
                const int personId = 3;
                const int establishmentId = 7;
                var command = new CreateAffiliationCommand
                {
                    PersonId = personId,
                    EstablishmentId = establishmentId,
                };
                var person = new Person
                {
                    RevisionId = personId,
                    Affiliations = new[]
                    {
                        new Affiliation { EstablishmentId = command.EstablishmentId }
                    }
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                entities.Setup(m => m.Read<Person>()).Returns(new[] { person }.AsQueryable);
                var validator = new CreateAffiliationValidator(entities.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PersonId");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    ValidatePerson.FailedBecausePersonIsAlreadyAffiliatedWithEstablishment,
                        personId, establishmentId));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_PersonId_IsNotAlreadyAffiliatedWithEstablishment()
            {
                const int personId = 3;
                const int establishmentId = 7;
                var command = new CreateAffiliationCommand
                {
                    PersonId = personId,
                    EstablishmentId = establishmentId,
                };
                var person = new Person
                {
                    RevisionId = personId,
                    Affiliations = new[]
                    {
                        new Affiliation { EstablishmentId = 1 }
                    }
                };
                var entities = new Mock<IQueryEntities>(MockBehavior.Strict).Initialize();
                entities.Setup(m => m.Read<Establishment>()).Returns(new Establishment[] { }.AsQueryable);
                entities.Setup(m => m.Read<Person>()).Returns(new[] { person }.AsQueryable);
                var validator = new CreateAffiliationValidator(entities.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PersonId");
                error.ShouldBeNull();
            }
        }
    }
}
