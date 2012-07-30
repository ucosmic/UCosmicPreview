using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.InstitutionalAgreements
{
    public static class InstitutionalAgreementFacts
    {
        [TestClass]
        public class TypeProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new InstitutionalAgreement { Type = value };
                entity.ShouldNotBeNull();
                entity.Type.ShouldEqual(value);
            }
        }

        [TestClass]
        public class StatusProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new InstitutionalAgreement { Status = value };
                entity.ShouldNotBeNull();
                entity.Status.ShouldEqual(value);
            }
        }

        [TestClass]
        public class IsExpirationEstimatedProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new InstitutionalAgreement { IsExpirationEstimated = value };
                entity.ShouldNotBeNull();
                entity.IsExpirationEstimated.ShouldEqual(value);
            }
        }

        [TestClass]
        public class UmbrellaProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementRuntimeEntity();
            }
            private class InstitutionalAgreementRuntimeEntity : InstitutionalAgreement
            {
                public override InstitutionalAgreement Umbrella
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class ChildrenProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<InstitutionalAgreement> { new InstitutionalAgreement() };
                var entity = new InstitutionalAgreement { Children = value };
                entity.ShouldNotBeNull();
                entity.Children.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementRuntimeEntity();
            }
            private class InstitutionalAgreementRuntimeEntity : InstitutionalAgreement
            {
                public override ICollection<InstitutionalAgreement> Children
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class AncestorsProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementRuntimeEntity();
            }
            private class InstitutionalAgreementRuntimeEntity : InstitutionalAgreement
            {
                public override ICollection<InstitutionalAgreementNode> Ancestors
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class OffspringProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<InstitutionalAgreementNode> { new InstitutionalAgreementNode() };
                var entity = new InstitutionalAgreement { Offspring = value };
                entity.ShouldNotBeNull();
                entity.Offspring.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementRuntimeEntity();
            }
            private class InstitutionalAgreementRuntimeEntity : InstitutionalAgreement
            {
                public override ICollection<InstitutionalAgreementNode> Offspring
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class ParticipantsProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<InstitutionalAgreementParticipant> { new InstitutionalAgreementParticipant() };
                var entity = new InstitutionalAgreement { Participants = value };
                entity.ShouldNotBeNull();
                entity.Participants.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementRuntimeEntity();
            }
            private class InstitutionalAgreementRuntimeEntity : InstitutionalAgreement
            {
                public override ICollection<InstitutionalAgreementParticipant> Participants
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class ContactsProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<InstitutionalAgreementContact> { new InstitutionalAgreementContact() };
                var entity = new InstitutionalAgreement { Contacts = value };
                entity.ShouldNotBeNull();
                entity.Contacts.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementRuntimeEntity();
            }
            private class InstitutionalAgreementRuntimeEntity : InstitutionalAgreement
            {
                public override ICollection<InstitutionalAgreementContact> Contacts
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class FilesProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<InstitutionalAgreementFile> { new InstitutionalAgreementFile() };
                var entity = new InstitutionalAgreement { Files = value };
                entity.ShouldNotBeNull();
                entity.Files.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new InstitutionalAgreementRuntimeEntity();
            }
            private class InstitutionalAgreementRuntimeEntity : InstitutionalAgreement
            {
                public override ICollection<InstitutionalAgreementFile> Files
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
