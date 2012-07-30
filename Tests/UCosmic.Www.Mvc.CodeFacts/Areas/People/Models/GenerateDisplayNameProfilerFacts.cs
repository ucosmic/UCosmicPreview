using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.People.Models
{
    public static class GenerateDisplayNameProfilerFacts
    {
        [TestClass]
        public class TheModelToQueryProfile
        {
            [TestMethod]
            public void MapsSalutation()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { Salutation = value };

                var query = Mapper.Map<GenerateDisplayNameQuery>(model);

                query.ShouldNotBeNull();
                query.Salutation.ShouldNotBeNull();
                query.Salutation.ShouldEqual(model.Salutation);
            }

            [TestMethod]
            public void MapsFirstName()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { FirstName = value };

                var query = Mapper.Map<GenerateDisplayNameQuery>(model);

                query.ShouldNotBeNull();
                query.FirstName.ShouldNotBeNull();
                query.FirstName.ShouldEqual(model.FirstName);
            }

            [TestMethod]
            public void MapsMiddleName()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { MiddleName = value };

                var query = Mapper.Map<GenerateDisplayNameQuery>(model);

                query.ShouldNotBeNull();
                query.MiddleName.ShouldNotBeNull();
                query.MiddleName.ShouldEqual(model.MiddleName);
            }

            [TestMethod]
            public void MapsLastName()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { LastName = value };

                var query = Mapper.Map<GenerateDisplayNameQuery>(model);

                query.ShouldNotBeNull();
                query.LastName.ShouldNotBeNull();
                query.LastName.ShouldEqual(model.LastName);
            }

            [TestMethod]
            public void MapsSuffix()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { Suffix = value };

                var query = Mapper.Map<GenerateDisplayNameQuery>(model);

                query.ShouldNotBeNull();
                query.Suffix.ShouldNotBeNull();
                query.Suffix.ShouldEqual(model.Suffix);
            }
        }
    }
}
