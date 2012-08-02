using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Establishments
{
    public static class EmailTemplateFacts
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
