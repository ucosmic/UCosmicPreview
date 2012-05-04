using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class MyHomeInfoFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheCanChangePasswordProperty
        {
            [TestMethod]
            public void IsTrue_WhenUserEdutPersonTargetedId_IsNull()
            {
                var model = new MyHomeInfo
                {
                    UserEduPersonTargetedId = null,
                };
                model.CanChangePassword.ShouldBeTrue();
            }

            [TestMethod]
            public void IsTrue_WhenUserEdutPersonTargetedId_IsEmptyString()
            {
                var model = new MyHomeInfo
                {
                    UserEduPersonTargetedId = string.Empty,
                };
                model.CanChangePassword.ShouldBeTrue();
            }

            [TestMethod]
            public void IsTrue_WhenUserEdutPersonTargetedId_IsWhiteSpace()
            {
                var model = new MyHomeInfo
                {
                    UserEduPersonTargetedId = "\n",
                };
                model.CanChangePassword.ShouldBeTrue();
            }

            [TestMethod]
            public void IsFalse_WhenUserEdutPersonTargetedId_IsNotNullOrWhiteSpace()
            {
                var model = new MyHomeInfo
                {
                    UserEduPersonTargetedId = "person targeted id",
                };
                model.CanChangePassword.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheMyEmailAddressNestedClass
        {
            [TestMethod]
            public void HasPublicDefaultConstructor_WithSettableProperties()
            {
                new MyHomeInfo
                {
                    Emails = new[]
                    {
                        new MyHomeInfo.MyEmailAddress
                        {
                            Value = "value",
                            IsConfirmed = true,
                            IsDefault = false,
                            Number = 6,
                        },
                    },
                };
            }
        }

        [TestClass]
        public class TheMyAffiliationNestedClass
        {
            [TestMethod]
            public void HasPublicDefaultConstructor_WithSettableProperties()
            {
                new MyHomeInfo
                {
                    Affiliations = new[]
                    {
                        new MyHomeInfo.MyAffiliation
                        {
                            EstablishmentId = 6,
                            IsAcknowledged = true,
                            IsClaimingEmployee = false,
                            IsClaimingStudent = false,
                            JobTitles = "job titles",
                            Establishment = new MyHomeInfo.MyAffiliation.EstablishmentModel
                            {
                                IsInstitution = true,
                                OfficialName = "official name",
                            },
                        },
                    },
                };
            }

            [TestMethod]
            public void JobTitles_IsDecoratedWith_DisplayFormat_UsingNullDisplayText()
            {
                Expression<Func<MyHomeInfo.MyAffiliation, string>> property = p => p.JobTitles;
                var attributes = property.GetAttributes<MyHomeInfo.MyAffiliation, string, DisplayFormatAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].NullDisplayText.ShouldEqual(MyHomeInfo.MyAffiliation.JobTitlesNullDisplayText);
            }
        }
    }
}
