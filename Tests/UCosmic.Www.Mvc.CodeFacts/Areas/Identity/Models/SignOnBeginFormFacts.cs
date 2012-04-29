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
    public class SignOnBeginFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheClass
        {
            [TestMethod]
            public void Implements_IReturnUrl()
            {
                var model = new SignOnBeginForm();
                model.ShouldImplement<IReturnUrl>();
            }
        }

        [TestClass]
        public class TheEmailAddressProperty
        {
            [TestMethod]
            public void IsDecoratedWith_DataType_UsingEmailAddress()
            {
                Expression<Func<SignOnBeginForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<SignOnBeginForm, string, DataTypeAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DataType.ShouldEqual(DataType.EmailAddress);
            }

            [TestMethod]
            public void IsDecoratedWith_UIHint_UsingSignOnEmailAddress()
            {
                Expression<Func<SignOnBeginForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<SignOnBeginForm, string, UIHintAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].UIHint.ShouldEqual("SignOnEmailAddress");
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<SignOnBeginForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<SignOnBeginForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(SignOnBeginForm.EmailAddressDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingPrompt()
            {
                Expression<Func<SignOnBeginForm, string>> property = p => p.EmailAddress;
                var attributes = property.GetAttributes<SignOnBeginForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Prompt.ShouldEqual(SignOnBeginForm.EmailAddressDisplayPrompt);
            }
        }

        [TestClass]
        public class TheReturnUrlProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<SignOnBeginForm, string>> property = p => p.ReturnUrl;
                var attributes = property.GetAttributes<SignOnBeginForm, string, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }
    }
}
