using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Identity
{
    // ReSharper disable UnusedMember.Global
    public class UserFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class GrantsProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<RoleGrant> { new RoleGrant() };
                var entity = new User { Grants = value };
                entity.ShouldNotBeNull();
                entity.Grants.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new UserRuntimeEntity();
            }
            private class UserRuntimeEntity : User
            {
                public override ICollection<RoleGrant> Grants
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
