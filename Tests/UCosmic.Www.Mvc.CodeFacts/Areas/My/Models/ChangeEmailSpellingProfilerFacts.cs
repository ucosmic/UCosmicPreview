using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;
using System.Threading;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    // ReSharper disable UnusedMember.Global
    public class ChangeEmailSpellingProfilerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheEntityToViewModelProfile
        {
            [TestMethod]
            public void MapsValue_ToValue()
            {
                const string value = "user@domain.tld";
                var entity = new EmailAddress { Value = value };

                var model = Mapper.Map<ChangeEmailSpellingForm>(entity);

                model.ShouldNotBeNull();
                model.Value.ShouldNotBeNull();
                model.Value.ShouldEqual(entity.Value);
            }

            [TestMethod]
            public void MapsValue_ToOldSpelling()
            {
                const string value = "user@domain.tld";
                var entity = new EmailAddress { Value = value };

                var model = Mapper.Map<ChangeEmailSpellingForm>(entity);

                model.ShouldNotBeNull();
                model.OldSpelling.ShouldNotBeNull();
                model.OldSpelling.ShouldEqual(entity.Value);
            }

            [TestMethod]
            public void MapsPersonUserName_ToPersonUserName()
            {
                const string userName = "user@domain.tld";
                var entity = new EmailAddress
                {
                    Person = new Person
                    {
                        User = new User { Name = userName }
                    }
                };

                var model = Mapper.Map<ChangeEmailSpellingForm>(entity);

                model.ShouldNotBeNull();
                model.PersonUserName.ShouldNotBeNull();
                model.PersonUserName.ShouldEqual(entity.Person.User.Name);
            }

            [TestMethod]
            public void MapsNumber_ToNumber()
            {
                const int number = 2;
                var entity = new EmailAddress { Number = number };

                var model = Mapper.Map<ChangeEmailSpellingForm>(entity);

                model.ShouldNotBeNull();
                model.Number.ShouldEqual(entity.Number);
            }

            [TestMethod]
            public void IgnoresReturnUrl()
            {
                var entity = new EmailAddress();

                var model = Mapper.Map<ChangeEmailSpellingForm>(entity);

                model.ShouldNotBeNull();
                model.ReturnUrl.ShouldBeNull();
            }
        }

        [TestClass]
        public class TheViewModelToCommandProfile
        {
            [TestMethod]
            public void Ignores_Principal()
            {
                const string userName = "user@domain.tld";
                var model = new ChangeEmailSpellingForm { PersonUserName = userName };

                var command = Mapper.Map<ChangeMyEmailSpellingCommand>(model);

                command.ShouldNotBeNull();
                command.Principal.ShouldBeNull();
            }

            [TestMethod]
            public void MapsValue_ToNewValue()
            {
                const string value = "user@domain.tld";
                var model = new ChangeEmailSpellingForm { Value = value };

                var command = Mapper.Map<ChangeMyEmailSpellingCommand>(model);

                command.ShouldNotBeNull();
                command.NewValue.ShouldNotBeNull();
                command.NewValue.ShouldEqual(model.Value);
            }

            [TestMethod]
            public void MapsNumber_ToNumber()
            {
                const int number = 2;
                var model = new ChangeEmailSpellingForm { Number = number };

                var command = Mapper.Map<ChangeMyEmailSpellingCommand>(model);

                command.ShouldNotBeNull();
                command.Number.ShouldEqual(model.Number);
            }

            [TestMethod]
            public void IgnoresChangedState()
            {
                var model = new ChangeEmailSpellingForm();

                var command = Mapper.Map<ChangeMyEmailSpellingCommand>(model);

                command.ShouldNotBeNull();
                command.ChangedState.ShouldBeFalse();
            }
        }
    }
}
