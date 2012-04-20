using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Establishments;

namespace UCosmic.Domain.People
{
    // ReSharper disable UnusedMember.Global
    public class AffiliationFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class PersonIdProperty
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
        public class PersonProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new AffiliationRuntimeEntity();
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
        public class EstablishmentIdProperty
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
        public class EstablishmentProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new AffiliationRuntimeEntity();
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
        public class JobTitlesProperty
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
        public class IsAcknowledgedProperty
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
        public class IsClaimingStudentProperty
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
        public class IsClaimingInternationalOfficeProperty
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
    }
}
