using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class MyHomeProfilerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheEntityToViewModelProfile
        {
            [TestMethod]
            public void MapsEmails_OrderingDefaultAtTop()
            {
                var entity = new Person
                {
                    Emails = new[]
                    {
                        new EmailAddress { IsDefault = false },
                        new EmailAddress { IsDefault = false },
                        new EmailAddress { IsDefault = true },
                    }
                };

                var model = Mapper.Map<MyHomeInfo>(entity);

                model.ShouldNotBeNull();
                model.Emails.Length.ShouldEqual(3);
                model.Emails.First().IsDefault.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsEmails_OrderingConfirmed_AboveOthers()
            {
                var entity = new Person
                {
                    Emails = new[]
                    {
                        new EmailAddress { IsConfirmed = false },
                        new EmailAddress { IsConfirmed = false },
                        new EmailAddress { IsConfirmed = true },
                    }
                };

                var model = Mapper.Map<MyHomeInfo>(entity);

                model.ShouldNotBeNull();
                model.Emails.Length.ShouldEqual(3);
                model.Emails.First().IsConfirmed.ShouldBeTrue();
            }

            [TestMethod]
            public void MapsEmails_OrderingByValue_AfterDefaultAndConfirmed()
            {
                var entity = new Person
                {
                    Emails = new[]
                    {
                        new EmailAddress { IsConfirmed = false, Value = "g1@domain.tld" },
                        new EmailAddress { IsConfirmed = false, Value = "b1@domain.tld" },
                        new EmailAddress { IsConfirmed = false, Value = "a2@domain.tld" },
                        new EmailAddress { IsConfirmed = true, Value = "h1@domain.tld" },
                        new EmailAddress { IsConfirmed = true, Value = "a1@domain.tld" },
                        new EmailAddress { IsDefault = true, Value = "d@domain.tld"},
                    }
                };

                var model = Mapper.Map<MyHomeInfo>(entity);

                model.ShouldNotBeNull();
                model.Emails.Length.ShouldEqual(6);
                model.Emails.First().IsDefault.ShouldBeTrue();
                model.Emails.First().Value.ShouldEqual("d@domain.tld");
                model.Emails.Skip(1).First().IsConfirmed.ShouldBeTrue();
                model.Emails.Skip(1).First().Value.ShouldEqual("a1@domain.tld");
                model.Emails.Skip(2).First().IsConfirmed.ShouldBeTrue();
                model.Emails.Skip(2).First().Value.ShouldEqual("h1@domain.tld");
                model.Emails.Skip(3).First().IsConfirmed.ShouldBeFalse();
                model.Emails.Skip(3).First().Value.ShouldEqual("a2@domain.tld");
                model.Emails.Skip(4).First().IsConfirmed.ShouldBeFalse();
                model.Emails.Skip(4).First().Value.ShouldEqual("b1@domain.tld");
                model.Emails.Skip(5).First().IsConfirmed.ShouldBeFalse();
                model.Emails.Skip(5).First().Value.ShouldEqual("g1@domain.tld");
            }

            [TestMethod]
            public void MapsUser_UserEduPersonTargetedId()
            {
                var entity = new Person
                {
                    User = new User
                    {
                        EduPersonTargetedId = "person targeted id",
                    },
                };

                var model = Mapper.Map<MyHomeInfo>(entity);

                model.ShouldNotBeNull();
                model.UserEduPersonTargetedId.ShouldEqual(entity.User.EduPersonTargetedId);
            }
        }
    }
}
