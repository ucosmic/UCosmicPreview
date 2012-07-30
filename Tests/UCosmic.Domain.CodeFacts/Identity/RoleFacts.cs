using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Identity
{
    public static class RoleFacts
    {
        [TestClass]
        public class GrantsProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<RoleGrant> { new RoleGrant() };
                var entity = new Role { Grants = value };
                entity.ShouldNotBeNull();
                entity.Grants.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new RoleRuntimeEntity();
            }
            private class RoleRuntimeEntity : Role
            {
                public override ICollection<RoleGrant> Grants
                {
                    get { return null; }
                    protected internal set { }
                }
            }
        }
    }
}
