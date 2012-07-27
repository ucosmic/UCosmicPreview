using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.People.Models
{
    // ReSharper disable UnusedMember.Global
    public class PersonInfoProfilerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheEntityToViewModelProfile
        {
            [TestMethod]
            public void MapsSalutation()
            {
                const string value = "test";
                var entity = new Person { Salutation = value };

                var model = Mapper.Map<PersonInfoModel>(entity);

                model.ShouldNotBeNull();
                model.Salutation.ShouldNotBeNull();
                model.Salutation.ShouldEqual(entity.Salutation);
            }

            [TestMethod]
            public void MapsEntityId()
            {
                var value = Guid.NewGuid();
                var entity = new Person { EntityId = value };

                var model = Mapper.Map<PersonInfoModel>(entity);

                model.ShouldNotBeNull();
                model.EntityId.ShouldNotEqual(Guid.Empty);
                model.EntityId.ShouldEqual(entity.EntityId);
            }

            [TestMethod]
            public void MapsFirstName()
            {
                const string value = "test";
                var entity = new Person { FirstName = value };

                var model = Mapper.Map<PersonInfoModel>(entity);

                model.ShouldNotBeNull();
                model.FirstName.ShouldNotBeNull();
                model.FirstName.ShouldEqual(entity.FirstName);
            }

            [TestMethod]
            public void MapsMiddleName()
            {
                const string value = "test";
                var entity = new Person { MiddleName = value };

                var model = Mapper.Map<PersonInfoModel>(entity);

                model.ShouldNotBeNull();
                model.MiddleName.ShouldNotBeNull();
                model.MiddleName.ShouldEqual(entity.MiddleName);
            }

            [TestMethod]
            public void MapsLastName()
            {
                const string value = "test";
                var entity = new Person { LastName = value };

                var model = Mapper.Map<PersonInfoModel>(entity);

                model.ShouldNotBeNull();
                model.LastName.ShouldNotBeNull();
                model.LastName.ShouldEqual(entity.LastName);
            }

            [TestMethod]
            public void MapsSuffix()
            {
                const string value = "test";
                var entity = new Person { Suffix = value };

                var model = Mapper.Map<PersonInfoModel>(entity);

                model.ShouldNotBeNull();
                model.Suffix.ShouldNotBeNull();
                model.Suffix.ShouldEqual(entity.Suffix);
            }

            [TestMethod]
            public void MapsDefaultEmail_ToNull_WhenPersonEmails_IsNull()
            {
                var entity = new Person();

                var model = Mapper.Map<PersonInfoModel>(entity);

                model.ShouldNotBeNull();
                model.DefaultEmail.ShouldBeNull();
            }

            [TestMethod]
            public void MapsDefaultEmail_ToNull_WhenPersonEmails_ContainsNoDefaultEmail()
            {
                var entity = new Person
                {
                    Emails = new[]
                    {
                        new EmailAddress(),
                        new EmailAddress(),
                        new EmailAddress(),
                    }
                };

                var model = Mapper.Map<PersonInfoModel>(entity);

                model.ShouldNotBeNull();
                model.DefaultEmail.ShouldBeNull();
            }

            [TestMethod]
            public void MapsDefaultEmail_ToDefaultEmail_WhenPersonEmails_ContainsDefaultEmail()
            {
                const string value = "user@domain.tld";
                var entity = new Person
                {
                    Emails = new[]
                    {
                        new EmailAddress(),
                        new EmailAddress
                        {
                            IsDefault = true,
                            Value = value,
                        },
                        new EmailAddress(),
                    }
                };

                var model = Mapper.Map<PersonInfoModel>(entity);

                model.ShouldNotBeNull();
                model.DefaultEmail.ShouldNotBeNull();
                model.DefaultEmail.ShouldEqual(value);
            }

            [TestMethod]
            public void AutoMapper_CanConstruct_AndSetProperties()
            {
                new PersonInfoModel
                {
                    EntityId = Guid.NewGuid(),
                    Salutation = null,
                    FirstName = null,
                    MiddleName = null,
                    LastName = null,
                    Suffix = null,
                    DefaultEmail = null,
                };
            }
        }
    }
}
