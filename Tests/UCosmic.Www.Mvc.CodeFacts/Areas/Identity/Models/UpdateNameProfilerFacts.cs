using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class UpdateNameProfilerFacts
    {
        [TestClass]
        public class TheEntityToModelProfile
        {
            [TestMethod]
            public void MapsDisplayName()
            {
                const string value = "test";
                var entity = new Person { DisplayName = value };

                var model = Mapper.Map<UpdateNameForm>(entity);

                model.ShouldNotBeNull();
                model.DisplayName.ShouldNotBeNull();
                model.DisplayName.ShouldEqual(entity.DisplayName);
            }

            [TestMethod]
            public void MapsIsDisplayNameDerived()
            {
                var entity = new Person { IsDisplayNameDerived = true };

                var model = Mapper.Map<UpdateNameForm>(entity);

                model.ShouldNotBeNull();
                model.IsDisplayNameDerived.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsSalutation()
            {
                const string value = "test";
                var entity = new Person { Salutation = value };

                var model = Mapper.Map<UpdateNameForm>(entity);

                model.ShouldNotBeNull();
                model.Salutation.ShouldNotBeNull();
                model.Salutation.ShouldEqual(entity.Salutation);
            }

            [TestMethod]
            public void MapsFirstName()
            {
                const string value = "test";
                var entity = new Person { FirstName = value };

                var model = Mapper.Map<UpdateNameForm>(entity);

                model.ShouldNotBeNull();
                model.FirstName.ShouldNotBeNull();
                model.FirstName.ShouldEqual(entity.FirstName);
            }

            [TestMethod]
            public void MapsMiddleName()
            {
                const string value = "test";
                var entity = new Person { MiddleName = value };

                var model = Mapper.Map<UpdateNameForm>(entity);

                model.ShouldNotBeNull();
                model.MiddleName.ShouldNotBeNull();
                model.MiddleName.ShouldEqual(entity.MiddleName);
            }

            [TestMethod]
            public void MapsLastName()
            {
                const string value = "test";
                var entity = new Person { LastName = value };

                var model = Mapper.Map<UpdateNameForm>(entity);

                model.ShouldNotBeNull();
                model.LastName.ShouldNotBeNull();
                model.LastName.ShouldEqual(entity.LastName);
            }

            [TestMethod]
            public void MapsSuffix()
            {
                const string value = "test";
                var entity = new Person { Suffix = value };

                var model = Mapper.Map<UpdateNameForm>(entity);

                model.ShouldNotBeNull();
                model.Suffix.ShouldNotBeNull();
                model.Suffix.ShouldEqual(entity.Suffix);
            }
        }

        [TestClass]
        public class TheModelToCommandProfile
        {
            [TestMethod]
            public void MapsDisplayName()
            {
                const string value = "test";
                var model = new UpdateNameForm { DisplayName = value };

                var command = Mapper.Map<UpdateMyNameCommand>(model);

                command.ShouldNotBeNull();
                command.DisplayName.ShouldNotBeNull();
                command.DisplayName.ShouldEqual(model.DisplayName);
            }

            [TestMethod]
            public void MapsIsDisplayNameDerived()
            {
                var model = new UpdateNameForm { IsDisplayNameDerived = true };

                var command = Mapper.Map<UpdateMyNameCommand>(model);

                command.ShouldNotBeNull();
                command.IsDisplayNameDerived.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsSalutation()
            {
                const string value = "test";
                var model = new UpdateNameForm { Salutation = value };

                var command = Mapper.Map<UpdateMyNameCommand>(model);

                command.ShouldNotBeNull();
                command.Salutation.ShouldNotBeNull();
                command.Salutation.ShouldEqual(model.Salutation);
            }

            [TestMethod]
            public void MapsFirstName()
            {
                const string value = "test";
                var model = new UpdateNameForm { FirstName = value };

                var command = Mapper.Map<UpdateMyNameCommand>(model);

                command.ShouldNotBeNull();
                command.FirstName.ShouldNotBeNull();
                command.FirstName.ShouldEqual(model.FirstName);
            }

            [TestMethod]
            public void MapsMiddleName()
            {
                const string value = "test";
                var model = new UpdateNameForm { MiddleName = value };

                var command = Mapper.Map<UpdateMyNameCommand>(model);

                command.ShouldNotBeNull();
                command.MiddleName.ShouldNotBeNull();
                command.MiddleName.ShouldEqual(model.MiddleName);
            }

            [TestMethod]
            public void MapsLastName()
            {
                const string value = "test";
                var model = new UpdateNameForm { LastName = value };

                var command = Mapper.Map<UpdateMyNameCommand>(model);

                command.ShouldNotBeNull();
                command.LastName.ShouldNotBeNull();
                command.LastName.ShouldEqual(model.LastName);
            }

            [TestMethod]
            public void MapsSuffix()
            {
                const string value = "test";
                var model = new UpdateNameForm { Suffix = value };

                var command = Mapper.Map<UpdateMyNameCommand>(model);

                command.ShouldNotBeNull();
                command.Suffix.ShouldNotBeNull();
                command.Suffix.ShouldEqual(model.Suffix);
            }

            [TestMethod]
            public void IgnoresPrincipal()
            {
                var model = new UpdateNameForm();

                var command = Mapper.Map<UpdateMyNameCommand>(model);

                command.ShouldNotBeNull();
                command.Principal.ShouldBeNull();
            }

            [TestMethod]
            public void IgnoresChangeCount()
            {
                var model = new UpdateNameForm();

                var command = Mapper.Map<UpdateMyNameCommand>(model);

                command.ShouldNotBeNull();
                command.ChangeCount.ShouldEqual(0);
            }
        }
    }
}
