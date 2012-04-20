using System;
using System.Linq;
using System.Linq.Expressions;
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
            public void IsInvalidWhen_EstablishmentId_DoesNotMatchEstablishment()
            {
                const int establishmentId = 6;
                var command = new CreateAffiliationCommand { EstablishmentId = establishmentId };
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.Is(EstablishmentQueryBasedOn(command))))
                    .Returns(null as Establishment);
                var validator = new CreateAffiliationValidator(queryProcessor.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EstablishmentId");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    CreateAffiliationValidator.FailedBecauseEstablishmentIdDoesNotMatchEstablishment,
                        establishmentId));
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void IsValidWhen_EstablishmentId_MatchesEstablishment()
            {
                const int establishmentId = 6;
                var command = new CreateAffiliationCommand { EstablishmentId = establishmentId };
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.Is(EstablishmentQueryBasedOn(command))))
                    .Returns(new Establishment { RevisionId = command.EstablishmentId });
                var validator = new CreateAffiliationValidator(queryProcessor.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "EstablishmentId");
                error.ShouldBeNull();
            }

            private static Expression<Func<GetEstablishmentByIdQuery, bool>> EstablishmentQueryBasedOn(CreateAffiliationCommand command)
            {
                Expression<Func<GetEstablishmentByIdQuery, bool>> establishmentQueryBasedOn = q =>
                    q.Id == command.EstablishmentId;
                return establishmentQueryBasedOn;
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
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(null as Person);
                var validator = new CreateAffiliationValidator(queryProcessor.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PersonId");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    CreateAffiliationValidator.FailedBecausePersonIdDoesNotMatchPerson,
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
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(new Person
                    {
                        Affiliations = new[]
                        {
                            new Affiliation { EstablishmentId = command.EstablishmentId }
                        }
                    });
                var validator = new CreateAffiliationValidator(queryProcessor.Object);

                var results = validator.Validate(command);

                results.IsValid.ShouldBeFalse();
                results.Errors.Count.ShouldBeInRange(1, int.MaxValue);
                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PersonId");
                error.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                error.ErrorMessage.ShouldEqual(string.Format(
                    CreateAffiliationValidator.FailedBecausePersonIsAlreadyAffiliatedWithEstablishment,
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
                var queryProcessor = new Mock<IProcessQueries>();
                queryProcessor.Setup(m => m.Execute(It.Is(PersonQueryBasedOn(command))))
                    .Returns(new Person
                    {
                        Affiliations = new[]
                        {
                            new Affiliation { EstablishmentId = 1 }
                        }
                    });
                var validator = new CreateAffiliationValidator(queryProcessor.Object);

                var results = validator.Validate(command);

                var error = results.Errors.SingleOrDefault(e => e.PropertyName == "PersonId");
                error.ShouldBeNull();
            }

            private static Expression<Func<GetPersonByIdQuery, bool>> PersonQueryBasedOn(CreateAffiliationCommand command)
            {
                Expression<Func<GetPersonByIdQuery, bool>> personQueryBasedOn = q =>
                    q.Id == command.PersonId;
                return personQueryBasedOn;
            }
        }
    }
}
