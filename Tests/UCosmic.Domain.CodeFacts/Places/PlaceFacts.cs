using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Places
{
    // ReSharper disable UnusedMember.Global
    public class PlaceFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class IsEarthProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new Place { IsEarth = value };
                entity.ShouldNotBeNull();
                entity.IsEarth.ShouldEqual(value);
            }
        }

        [TestClass]
        public class IsContinentProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new Place { IsContinent = value };
                entity.ShouldNotBeNull();
                entity.IsContinent.ShouldEqual(value);
            }
        }

        [TestClass]
        public class IsCountryProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new Place { IsCountry = value };
                entity.ShouldNotBeNull();
                entity.IsCountry.ShouldEqual(value);
            }
        }

        [TestClass]
        public class IsAdmin1Property
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new Place { IsAdmin1 = value };
                entity.ShouldNotBeNull();
                entity.IsAdmin1.ShouldEqual(value);
            }
        }

        [TestClass]
        public class IsAdmin2Property
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new Place { IsAdmin2 = value };
                entity.ShouldNotBeNull();
                entity.IsAdmin2.ShouldEqual(value);
            }
        }

        [TestClass]
        public class IsAdmin3Property
        {
            [TestMethod]
            public void HasGetSet()
            {
                const bool value = true;
                var entity = new Place { IsAdmin3 = value };
                entity.ShouldNotBeNull();
                entity.IsAdmin3.ShouldEqual(value);
            }
        }

        [TestClass]
        public class BoundingBoxProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new BoundingBox();
                var entity = new Place { BoundingBox = value };
                entity.ShouldNotBeNull();
                entity.BoundingBox.ShouldEqual(value);
            }
        }

        [TestClass]
        public class CenterProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new Coordinates();
                var entity = new Place { Center = value };
                entity.ShouldNotBeNull();
                entity.Center.ShouldEqual(value);
            }
        }

        [TestClass]
        public class ParentProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new PlaceRuntimeEntity();
            }
            private class PlaceRuntimeEntity : Place
            {
                public override Place Parent
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
                var value = new List<Place> { new Place() };
                var entity = new Place { Children = value };
                entity.ShouldNotBeNull();
                entity.Children.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new PlaceRuntimeEntity();
            }
            private class PlaceRuntimeEntity : Place
            {
                public override ICollection<Place> Children
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
                new PlaceRuntimeEntity();
            }
            private class PlaceRuntimeEntity : Place
            {
                public override ICollection<PlaceNode> Ancestors
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
            public void IsVirtual()
            {
                new PlaceRuntimeEntity();
            }
            private class PlaceRuntimeEntity : Place
            {
                public override ICollection<PlaceNode> Offspring
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class GeoNamesToponymProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new PlaceRuntimeEntity();
            }
            private class PlaceRuntimeEntity : Place
            {
                public override GeoNamesToponym GeoNamesToponym
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class GeoPlanetPlaceProperty
        {
            [TestMethod]
            public void IsVirtual()
            {
                new PlaceRuntimeEntity();
            }
            private class PlaceRuntimeEntity : Place
            {
                public override GeoPlanetPlace GeoPlanetPlace
                {
                    get { return null; }
                    set { }
                }
            }
        }

        [TestClass]
        public class NamesProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                var value = new List<PlaceName> { new PlaceName(), new PlaceName() };
                var entity = new Place { Names = value };
                entity.ShouldNotBeNull();
                entity.Names.ShouldEqual(value);
            }

            [TestMethod]
            public void IsVirtual()
            {
                new PlaceRuntimeEntity();
            }
            private class PlaceRuntimeEntity : Place
            {
                public override ICollection<PlaceName> Names
                {
                    get { return null; }
                    set { }
                }
            }
        }
    }
}
