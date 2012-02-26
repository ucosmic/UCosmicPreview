using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Places
{
    // ReSharper disable UnusedMember.Global
    public class BoundingBoxFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class HasValueProperty
        {
            [TestMethod]
            public void ReturnsFalse_WhenNortheast_IsNull()
            {
                var entity = new BoundingBox
                {
                    Northeast = null,
                    Southwest = new Coordinates{Latitude = 1, Longitude = 1 }
                };
                var result = entity.HasValue;
                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenSouthwest_IsNull()
            {
                var entity = new BoundingBox
                {
                    Northeast = new Coordinates { Latitude = 1, Longitude = 1 },
                    Southwest = null,
                };
                var result = entity.HasValue;
                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenNortheast_HasValue_IsFalse()
            {
                var entity = new BoundingBox
                {
                    Northeast = new Coordinates { Latitude = null, Longitude = 1 },
                    Southwest = new Coordinates { Latitude = 1, Longitude = 1 }
                };
                var result = entity.HasValue;
                result.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenSouthwest_HasValue_IsFalse()
            {
                var entity = new BoundingBox
                {
                    Northeast = new Coordinates { Latitude = 1, Longitude = 1 },
                    Southwest = new Coordinates { Latitude = 1, Longitude = null }
                };
                var result = entity.HasValue;
                result.ShouldBeFalse();
            }
        }
    }
}
