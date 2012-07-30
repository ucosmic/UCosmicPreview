using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Www.Mvc.Models;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    public static class SignOnFormFacts
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Implements_IReturnUrl()
            {
                var model = new SignOnForm();
                model.ShouldImplement<IReturnUrl>();
            }
        }

        [TestClass]
        public class TheEmailAddressProperty
        {
            [TestMethod]
            public void IsDecoratedWith_DataType_UsingEmailAddress()
            {
                Expression<Func<SignOnForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<SignOnForm, string, DataTypeAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DataType.ShouldEqual(DataType.EmailAddress);
            }

            [TestMethod]
            public void IsDecoratedWith_UIHint_UsingEmailAddress()
            {
                Expression<Func<SignOnForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<SignOnForm, string, UIHintAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].UIHint.ShouldEqual(SignOnForm.EmailAddressUiHint);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<SignOnForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<SignOnForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(SignOnForm.EmailAddressDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingPrompt()
            {
                Expression<Func<SignOnForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<SignOnForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Prompt.ShouldEqual(SignOnForm.EmailAddressDisplayPrompt);
            }
        }

        [TestClass]
        public class TheReturnUrlProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<SignOnForm, string>> property = p => p.ReturnUrl;
                var attributes = property.GetAttributes<SignOnForm, string, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }
    }
}
