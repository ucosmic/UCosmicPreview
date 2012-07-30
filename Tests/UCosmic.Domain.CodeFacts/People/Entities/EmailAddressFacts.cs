using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.People
{
    public static class EmailAddressFacts
    {
        [TestClass]
        public class PersonIdProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const int value = 8;
                var entity = new EmailAddress { PersonId = value };
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
                new EmailAddressRuntimeEntity();
            }
            private class EmailAddressRuntimeEntity : EmailAddress
            {
                public override Person Person
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }

        [TestClass]
        public class ConfirmationsProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new EmailAddressRuntimeEntity();
            }
            private class EmailAddressRuntimeEntity : EmailAddress
            {
                public override ICollection<EmailConfirmation> Confirmations
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }

    }
}
