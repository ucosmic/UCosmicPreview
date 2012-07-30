using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Places
{
    public static class CoordinatesFacts
    {
        [TestClass]
        public class HasValueProperty
        {
            [TestMethod]
            public void ReturnsFalse_WhenLatitude_IsNull()
            {
                var entity = new Coordinates { Latitude = null, Longitude = 1 };

                entity.ShouldNotBeNull();
                entity.HasValue.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsFalse_WhenLongitude_IsNull()
            {
                var entity = new Coordinates { Latitude = 1, Longitude = null };

                entity.ShouldNotBeNull();
                entity.HasValue.ShouldBeFalse();
            }

            [TestMethod]
            public void ReturnsTrue_WhenBothLatitudeAndLongitude_AreNotNull()
            {
                var entity = new Coordinates { Latitude = 1, Longitude = 1 };

                entity.ShouldNotBeNull();
                entity.HasValue.ShouldBeTrue();
            }
        }
    }
}
