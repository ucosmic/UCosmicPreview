using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Should;

namespace UCosmic.Domain.Establishments
{
    // ReSharper disable UnusedMember.Global
    public class EstablishmentChangerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class Constructor
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ThrowsArgumentNullException_WhenObjectCommanderArg_IsNull()
            {
                new EstablishmentChanger(null, null);
            }
        }

        [TestClass]
        public class DeriveNodesMethod
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentNullException))]
            public void ThrowsArgumentNullException_WhenEstablishmentArg_IsNull()
            {
                var commander = new Mock<ICommandObjects>();
                new EstablishmentChanger(commander.Object, null).DeriveNodes(null, null);
            }

            [TestMethod]
            public void AddsAncestorNodesToChildren_Recursively()
            {
                // arrange
                var rootEstablishment = CreateTestEstablishmentHierarchy();

                rootEstablishment.ShouldNotBeNull();
                rootEstablishment.Children.ShouldNotBeNull();
                rootEstablishment.Children.Count.ShouldEqual(5);

                // act
                new EstablishmentChanger(new Mock<ICommandObjects>().Object, new Mock<IQueryEntities>().Object)
                    .DeriveNodes(rootEstablishment, null);

                // assert
                var level1Siblings = rootEstablishment.Children.ToList();
                level1Siblings.ForEach(child1 =>
                {
                    child1.ShouldNotBeNull();
                    child1.Ancestors.Count.ShouldEqual(1);
                    child1.Ancestors.First().Ancestor.ShouldEqual(rootEstablishment);
                    child1.Ancestors.First().Separation.ShouldEqual(1);
                    child1.Ancestors.First().Offspring.ShouldEqual(child1);

                    child1.Children.ToList().ForEach(child2 =>
                    {
                        child2.ShouldNotBeNull();
                        child2.Ancestors.Count.ShouldEqual(2);
                        var ancestor21 = child2.Ancestors.Single(a => a.Separation == 1);
                        var ancestor22 = child2.Ancestors.Single(a => a.Separation == 2);
                        ancestor21.Ancestor.ShouldEqual(child1);
                        ancestor21.Offspring.ShouldEqual(child2);
                        ancestor22.Ancestor.ShouldEqual(rootEstablishment);
                        ancestor22.Offspring.ShouldEqual(child2);

                        child2.Children.ToList().ForEach(child3 =>
                        {
                            child3.ShouldNotBeNull();
                            child3.Ancestors.Count.ShouldEqual(3);
                            var ancestor31 = child3.Ancestors.Single(a => a.Separation == 1);
                            var ancestor32 = child3.Ancestors.Single(a => a.Separation == 2);
                            var ancestor33 = child3.Ancestors.Single(a => a.Separation == 3);
                            ancestor31.Ancestor.ShouldEqual(child2);
                            ancestor31.Offspring.ShouldEqual(child3);
                            ancestor32.Ancestor.ShouldEqual(child1);
                            ancestor32.Offspring.ShouldEqual(child3);
                            ancestor33.Ancestor.ShouldEqual(rootEstablishment);
                            ancestor33.Offspring.ShouldEqual(child3);

                            child3.Children.ToList().ForEach(child4 =>
                            {
                                child4.ShouldNotBeNull();
                                child4.Ancestors.Count.ShouldEqual(4);
                                var ancestor41 = child4.Ancestors.Single(a => a.Separation == 1);
                                var ancestor42 = child4.Ancestors.Single(a => a.Separation == 2);
                                var ancestor43 = child4.Ancestors.Single(a => a.Separation == 3);
                                var ancestor44 = child4.Ancestors.Single(a => a.Separation == 4);
                                ancestor41.Ancestor.ShouldEqual(child3);
                                ancestor41.Offspring.ShouldEqual(child4);
                                ancestor42.Ancestor.ShouldEqual(child2);
                                ancestor42.Offspring.ShouldEqual(child4);
                                ancestor43.Ancestor.ShouldEqual(child1);
                                ancestor43.Offspring.ShouldEqual(child4);
                                ancestor44.Ancestor.ShouldEqual(rootEstablishment);
                                ancestor44.Offspring.ShouldEqual(child4);

                                child4.Children.ToList().ForEach(child5 =>
                                {
                                    child5.ShouldNotBeNull();
                                    child5.Ancestors.Count.ShouldEqual(4);
                                    var ancestor51 = child5.Ancestors.Single(a => a.Separation == 1);
                                    var ancestor52 = child5.Ancestors.Single(a => a.Separation == 2);
                                    var ancestor53 = child5.Ancestors.Single(a => a.Separation == 3);
                                    var ancestor54 = child5.Ancestors.Single(a => a.Separation == 4);
                                    var ancestor55 = child5.Ancestors.Single(a => a.Separation == 5);
                                    ancestor51.Ancestor.ShouldEqual(child4);
                                    ancestor51.Offspring.ShouldEqual(child5);
                                    ancestor52.Ancestor.ShouldEqual(child3);
                                    ancestor52.Offspring.ShouldEqual(child5);
                                    ancestor53.Ancestor.ShouldEqual(child2);
                                    ancestor53.Offspring.ShouldEqual(child5);
                                    ancestor54.Ancestor.ShouldEqual(child1);
                                    ancestor54.Offspring.ShouldEqual(child5);
                                    ancestor55.Ancestor.ShouldEqual(rootEstablishment);
                                    ancestor55.Offspring.ShouldEqual(child5);
                                    child5.Children.ShouldBeNull();
                                });
                            });
                        });
                    });
                });
            }
        }

        #region CreateTestEstablishmentHierarchy

        private static Establishment CreateTestEstablishmentHierarchy(
            string prefix = "A", int maxDepth = 5, int children = 5,
            string nameFormat = "{0} Level {1} Establishment {2}")
        {
            var root = CreateTestEstablishments(prefix, 0, maxDepth, children, nameFormat).Single();
            return root;
        }

        private static ICollection<Establishment> CreateTestEstablishments(
            string prefix, int currentDepth, int maxDepth, int children, string nameFormat)
        {
            if (currentDepth == maxDepth) return null;

            if (currentDepth == 0)
            {
                var root = new Establishment
                {
                    OfficialName = CreateTestEstablishmentName(prefix, currentDepth, 0, nameFormat),
                    Children = CreateTestEstablishments(prefix, 1, maxDepth, children, nameFormat)
                };
                return new List<Establishment> { root };
            }

            var establishments = new List<Establishment>();
            for (var i = 1; i <= children; i++)
            {
                var establishment = new Establishment
                {
                    OfficialName = CreateTestEstablishmentName(prefix, currentDepth, i, nameFormat),
                    Children = CreateTestEstablishments(prefix, currentDepth + 1, maxDepth, children, nameFormat)
                };
                establishments.Add(establishment);
            }

            return establishments;
        }

        private static string CreateTestEstablishmentName(string prefix, int depth, int ordinal, string nameFormat)
        {
            var name = string.Format(nameFormat, prefix, depth, ordinal);
            if (depth == 0)
                name = name.Substring(0, name.Length - 2);
            return name;
        }

        #endregion
    }
}
