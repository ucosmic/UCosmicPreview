using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.People.Models
{
    // ReSharper disable UnusedMember.Global
    public class GenerateDisplayNameProfilerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheViewModelToQueryProfile
        {
            [TestMethod]
            public void MapsSalutation()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { Salutation = value };

                var command = Mapper.Map<GenerateDisplayNameQuery>(model);

                command.ShouldNotBeNull();
                command.Salutation.ShouldNotBeNull();
                command.Salutation.ShouldEqual(model.Salutation);
            }

            [TestMethod]
            public void MapsFirstName()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { FirstName = value };

                var command = Mapper.Map<GenerateDisplayNameQuery>(model);

                command.ShouldNotBeNull();
                command.FirstName.ShouldNotBeNull();
                command.FirstName.ShouldEqual(model.FirstName);
            }

            [TestMethod]
            public void MapsMiddleName()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { MiddleName = value };

                var command = Mapper.Map<GenerateDisplayNameQuery>(model);

                command.ShouldNotBeNull();
                command.MiddleName.ShouldNotBeNull();
                command.MiddleName.ShouldEqual(model.MiddleName);
            }

            [TestMethod]
            public void MapsLastName()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { LastName = value };

                var command = Mapper.Map<GenerateDisplayNameQuery>(model);

                command.ShouldNotBeNull();
                command.LastName.ShouldNotBeNull();
                command.LastName.ShouldEqual(model.LastName);
            }

            [TestMethod]
            public void MapsSuffix()
            {
                const string value = "test";
                var model = new GenerateDisplayNameForm { Suffix = value };

                var command = Mapper.Map<GenerateDisplayNameQuery>(model);

                command.ShouldNotBeNull();
                command.Suffix.ShouldNotBeNull();
                command.Suffix.ShouldEqual(model.Suffix);
            }
        }
    }
}
