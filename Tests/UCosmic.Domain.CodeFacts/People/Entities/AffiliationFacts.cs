using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.People
{
    public static class AffiliationFacts
    {
        [TestClass]
        public class ThePersonIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 14;
                var entity = new Affiliation { PersonId = value };
                entity.ShouldNotBeNull();
                entity.PersonId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ThePersonProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                var obj = new AffiliationRuntimeEntity();
                obj.ShouldNotBeNull();
            }
            private class AffiliationRuntimeEntity : Affiliation
            {
                public override Person Person
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }

        [TestClass]
        public class TheEstablishmentIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 6;
                var entity = new Affiliation { EstablishmentId = value };
                entity.ShouldNotBeNull();
                entity.EstablishmentId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheEstablishmentProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                var obj = new AffiliationRuntimeEntity();
                obj.ShouldNotBeNull();
            }
            private class AffiliationRuntimeEntity : Affiliation
            {
                public override Establishment Establishment
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }

        [TestClass]
        public class TheJobTitlesProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new Affiliation { JobTitles = value };
                entity.ShouldNotBeNull();
                entity.JobTitles.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheIsAcknowledgedProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new Affiliation { IsAcknowledged = value };
                entity.ShouldNotBeNull();
                entity.IsAcknowledged.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheIsClaimingStudentProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new Affiliation { IsClaimingStudent = value };
                entity.ShouldNotBeNull();
                entity.IsClaimingStudent.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheIsClaimingInternationalOfficeProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new Affiliation { IsClaimingInternationalOffice = value };
                entity.ShouldNotBeNull();
                entity.IsClaimingInternationalOffice.ShouldEqual(value);
            }
        }

        [TestClass]
        public class TheToStringProperty
        {
            [TestMethod]
            public void ReturnsFriendlyInfo()
            {
                var entity = new Affiliation
                {
                    Person = new Person
                    {
                        DisplayName = "display name",
                    },
                    Establishment = new Establishment
                    {
                        OfficialName = "official name",
                    },
                };

                var result = entity.ToString();

                result.ShouldEqual(string.Format("{0} - {1}",
                    entity.Person.DisplayName, entity.Establishment.OfficialName));
            }
        }
    }
}
