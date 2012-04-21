using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    // ReSharper disable UnusedMember.Global
    public class ProfileInfoFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheCanChangePasswordProperty
        {
            [TestMethod]
            public void IsTrue_WhenUserEdutPersonTargetedId_IsNull()
            {
                var model = new ProfileInfo
                {
                    UserEduPersonTargetedId = null,
                };
                model.CanChangePassword.ShouldBeTrue();
            }

            [TestMethod]
            public void IsTrue_WhenUserEdutPersonTargetedId_IsEmptyString()
            {
                var model = new ProfileInfo
                {
                    UserEduPersonTargetedId = string.Empty,
                };
                model.CanChangePassword.ShouldBeTrue();
            }

            [TestMethod]
            public void IsTrue_WhenUserEdutPersonTargetedId_IsWhiteSpace()
            {
                var model = new ProfileInfo
                {
                    UserEduPersonTargetedId = "\n",
                };
                model.CanChangePassword.ShouldBeTrue();
            }

            [TestMethod]
            public void IsFalse_WhenUserEdutPersonTargetedId_IsNotNullOrWhiteSpace()
            {
                var model = new ProfileInfo
                {
                    UserEduPersonTargetedId = "person targeted id",
                };
                model.CanChangePassword.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheAffiliationInfoJobTitlesProperty
        {
            [TestMethod]
            public void IsDecoratedWith_DisplayFormat_UsingNullDisplayText()
            {
                Expression<Func<ProfileInfo.AffiliationInfo, string>> property = p => p.JobTitles;
                var attributes = property.GetAttributes<ProfileInfo.AffiliationInfo, string, DisplayFormatAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].NullDisplayText.ShouldEqual(ProfileInfo.AffiliationInfo.JobTitlesNullDisplayText);
            }
        }
    }
}
