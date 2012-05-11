using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Domain.People;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class ConfirmEmailFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheTokenProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<ConfirmEmailForm, Guid>> property = p => p.Token;
                var attributes = property.GetAttributes<ConfirmEmailForm, Guid, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheSecretCodeProperty
        {
            [TestMethod]
            public void IsDecoratedWith_DataType_UsingPassword()
            {
                Expression<Func<ConfirmEmailForm, string>> property = p => p.SecretCode;
                var attributes = property.GetAttributes<ConfirmEmailForm, string, DataTypeAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DataType.ShouldEqual(DataType.Password);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<ConfirmEmailForm, string>> property = p => p.SecretCode;
                var attributes = property.GetAttributes<ConfirmEmailForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(ConfirmEmailForm.SecretCodeDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingPrompt()
            {
                Expression<Func<ConfirmEmailForm, string>> property = p => p.SecretCode;
                var attributes = property.GetAttributes<ConfirmEmailForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Prompt.ShouldEqual(ConfirmEmailForm.SecretCodeDisplayPrompt);
            }

            [TestMethod]
            public void IsDecoratedWith_Remote_UsingHttpMethod_Post()
            {
                Expression<Func<ConfirmEmailForm, string>> property = p => p.SecretCode;
                var attributes = property.GetAttributes<ConfirmEmailForm, string, RemoteAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].HttpMethod.ShouldEqual("POST");
            }

            [TestMethod]
            public void IsDecoratedWith_Remote_UsingAdditionalFields_Token_Intent()
            {
                Expression<Func<ConfirmEmailForm, string>> property = p => p.SecretCode;
                var attributes = property.GetAttributes<ConfirmEmailForm, string, RemoteAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].AdditionalFields.ShouldEqual("Token,Intent");
            }
        }

        [TestClass]
        public class TheIntentProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<ConfirmEmailForm, EmailConfirmationIntent>> property = p => p.Intent;
                var attributes = property.GetAttributes<ConfirmEmailForm, EmailConfirmationIntent, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }

        [TestClass]
        public class TheIsUrlConfirmationProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput_Using_FalseDisplayValue()
            {
                Expression<Func<ConfirmEmailForm, bool>> property = p => p.IsUrlConfirmation;
                var attributes = property.GetAttributes<ConfirmEmailForm, bool, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }
    }
}
