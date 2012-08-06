using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Places
{
    public static class BoundingBoxFacts
    {
        [TestClass]
        public class HasValueProperty
        {
            [TestMethod]
            public void ReturnsFalse_WhenNortheast_IsNull()
            {
                var entity = new BoundingBox(null, null, 1, 1);
                var result = entity.HasValue;
                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenSouthwest_IsNull()
            {
                var entity = new BoundingBox(1, 1, null, null);
                var result = entity.HasValue;
                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenNortheast_HasValue_IsFalse()
            {
                var entity = new BoundingBox(null, 1, 1, 1);
                var result = entity.HasValue;
                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenSouthwest_HasValue_IsFalse()
            {
                var entity = new BoundingBox(1, 1, 1, null);
                var result = entity.HasValue;
                result.ShouldBeFalse();
            }
        }
    }
}
