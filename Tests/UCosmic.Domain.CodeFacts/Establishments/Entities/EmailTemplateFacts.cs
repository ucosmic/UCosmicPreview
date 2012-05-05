using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Establishments
{
    // ReSharper disable UnusedMember.Global
    public class EmailTemplateFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class EstablishmentIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 9;
                var entity = new EmailTemplate { EstablishmentId = value };
                entity.ShouldNotBeNull();
                entity.EstablishmentId.ShouldEqual(value);
            }
        }

        [TestClass]
        public class EstablishmentProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Establishment();
                var entity = new EmailTemplate { Establishment = value };
                entity.ShouldNotBeNull();
                entity.Establishment.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new EmailTemplateRuntimeEntity();
            }
            private class EmailTemplateRuntimeEntity : EmailTemplate
            {
                public override Establishment Establishment
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class FromAddressProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new EmailTemplate { FromAddress = value };
                entity.ShouldNotBeNull();
                entity.FromAddress.ShouldEqual(value);
            }
        }

        [TestClass]
        public class FromDisplayNameProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new EmailTemplate { FromDisplayName = value };
                entity.ShouldNotBeNull();
                entity.FromDisplayName.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ReplyToAddressProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new EmailTemplate { ReplyToAddress = value };
                entity.ShouldNotBeNull();
                entity.ReplyToAddress.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ReplyToDisplayNameProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new EmailTemplate { ReplyToDisplayName = value };
                entity.ShouldNotBeNull();
                entity.ReplyToDisplayName.ShouldEqual(value);
            }
        }
    }
}
