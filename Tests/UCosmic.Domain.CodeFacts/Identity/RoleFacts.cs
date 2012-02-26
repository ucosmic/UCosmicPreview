using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Identity
{
    // ReSharper disable UnusedMember.Global
    public class RoleFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class GrantsProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<RoleGrant> { new RoleGrant() };
                var entity = new Role(null) { Grants = value };
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
                    set { }
                }
            }
        }
    }
}
