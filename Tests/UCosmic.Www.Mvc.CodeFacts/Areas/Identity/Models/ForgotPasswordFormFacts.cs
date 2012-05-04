using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class ForgotPasswordFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Implements_IReturnUrl()
            {
                var model = new ForgotPasswordForm();
                model.ShouldImplement<IReturnUrl>();
            }
        }

        [TestClass]
        public class TheEmailAddressProperty
        {
            [TestMethod]
            public void IsDecoratedWith_DataType_UsingEmailAddress()
            {
                Expression<Func<ForgotPasswordForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<ForgotPasswordForm, string, DataTypeAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DataType.ShouldEqual(DataType.EmailAddress);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<ForgotPasswordForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<ForgotPasswordForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(ForgotPasswordForm.EmailAddressDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingPrompt()
            {
                Expression<Func<ForgotPasswordForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<ForgotPasswordForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Prompt.ShouldEqual(ForgotPasswordForm.EmailAddressDisplayPrompt);
            }

            [TestMethod]
            public void IsDecoratedWith_Remote_UsingHttpMethod_Post()
            {
                Expression<Func<ForgotPasswordForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<ForgotPasswordForm, string, RemoteAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].HttpMethod.ShouldEqual("POST");
            }

            [TestMethod]
            public void IsDecoratedWith_Remote_UsingNoAdditionalFields()
            {
                Expression<Func<ForgotPasswordForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<ForgotPasswordForm, string, RemoteAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].AdditionalFields.ShouldEqual(string.Empty);
            }
        }

        [TestClass]
        public class TheReturnUrlProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<ForgotPasswordForm, string>> property = p => p.ReturnUrl;
                var attributes = property.GetAttributes<ForgotPasswordForm, string, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }

            [TestMethod]
            public void HasPublicSetter()
            {
                new ForgotPasswordForm
                {
                    ReturnUrl = "/path/to/resource"
                };
            }
        }
    }
}
