using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.My.Models
{
    // ReSharper disable UnusedMember.Global
    public class UpdateEmailValueFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Implements_IReturnUrl()
            {
                var model = new UpdateEmailValueForm();
                model.ShouldImplement<IReturnUrl>();
            }
        }

        [TestClass]
        public class TheValueProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName_NewSpelling()
            {
                Expression<Func<UpdateEmailValueForm, string>> property = p => p.Value;
                var attributes = property.GetAttributes<UpdateEmailValueForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateEmailValueForm.ValueDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_Remote_UsingHttpMethod_Post()
            {
                Expression<Func<UpdateEmailValueForm, string>> property = p => p.Value;
                var attributes = property.GetAttributes<UpdateEmailValueForm, string, RemoteAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].HttpMethod.ShouldEqual("POST");
            }

            [TestMethod]
            public void IsDecoratedWith_Remote_UsingAdditionalFields_PersonUserName_AndNumber()
            {
                Expression<Func<UpdateEmailValueForm, string>> property = p => p.Value;
                var attributes = property.GetAttributes<UpdateEmailValueForm, string, RemoteAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].AdditionalFields.ShouldEqual("PersonUserName,Number");
            }
        }

        [TestClass]
        public class TheOldSpellingProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName_CurrentSpelling()
            {
                Expression<Func<UpdateEmailValueForm, string>> property = p => p.OldSpelling;
                var attributes = property.GetAttributes<UpdateEmailValueForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateEmailValueForm.OldSpellingDisplayName);
            }

            [TestMethod]
            public void HasPublicSetter()
            {
                new UpdateEmailValueForm
                {
                    OldSpelling = "user@domain.tld"
                };
            }
        }

        [TestClass]
        public class ThePersonUserNameProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput()
            {
                Expression<Func<UpdateEmailValueForm, string>> property = p => p.PersonUserName;
                var attributes = property.GetAttributes<UpdateEmailValueForm, string, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheReturnUrlProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput()
            {
                Expression<Func<UpdateEmailValueForm, string>> property = p => p.ReturnUrl;
                var attributes = property.GetAttributes<UpdateEmailValueForm, string, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheNumberProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput()
            {
                Expression<Func<UpdateEmailValueForm, int>> property = p => p.Number;
                var attributes = property.GetAttributes<UpdateEmailValueForm, int, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }
    }
}
