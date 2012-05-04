using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class ResetPasswordFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheTokenProperty
        {
            [TestMethod]
            public void IsDecoratedWith_HiddenInput()
            {
                Expression<Func<ResetPasswordForm, Guid>> property = p => p.Token;
                var attributes = property.GetAttributes<ResetPasswordForm, Guid, HiddenInputAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DisplayValue.ShouldBeFalse();
            }
        }

        [TestClass]
        public class ThePasswordProperty
        {
            [TestMethod]
            public void IsDecoratedWith_UIHintUsing_StrengthMeteredPassword()
            {
                Expression<Func<ResetPasswordForm, string>> property = p => p.Password;
                var attributes = property.GetAttributes<ResetPasswordForm, string, UIHintAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].UIHint.ShouldEqual(ResetPasswordForm.PasswordUiHint);
            }

            [TestMethod]
            public void IsDecoratedWith_DataType_UsingPassword()
            {
                Expression<Func<ResetPasswordForm, string>> property = p => p.Password;
                var attributes = property.GetAttributes<ResetPasswordForm, string, DataTypeAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DataType.ShouldEqual(DataType.Password);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<ResetPasswordForm, string>> property = p => p.Password;
                var attributes = property.GetAttributes<ResetPasswordForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(ResetPasswordForm.PasswordDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingPrompt()
            {
                Expression<Func<ResetPasswordForm, string>> property = p => p.Password;
                var attributes = property.GetAttributes<ResetPasswordForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Prompt.ShouldEqual(ResetPasswordForm.PasswordDisplayPrompt);
            }
        }

        [TestClass]
        public class ThePasswordConfirmationProperty
        {
            [TestMethod]
            public void IsDecoratedWith_DataType_UsingPassword()
            {
                Expression<Func<ResetPasswordForm, string>> property = p => p.PasswordConfirmation;
                var attributes = property.GetAttributes<ResetPasswordForm, string, DataTypeAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].DataType.ShouldEqual(DataType.Password);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<ResetPasswordForm, string>> property = p => p.PasswordConfirmation;
                var attributes = property.GetAttributes<ResetPasswordForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(ResetPasswordForm.PasswordConfirmationDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingPrompt()
            {
                Expression<Func<ResetPasswordForm, string>> property = p => p.PasswordConfirmation;
                var attributes = property.GetAttributes<ResetPasswordForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Prompt.ShouldEqual(ResetPasswordForm.PasswordConfirmationDisplayPrompt);
            }

            [TestMethod]
            public void IsDecoratedWith_Remote_UsingHttpMethod_Post()
            {
                Expression<Func<ResetPasswordForm, string>> property = p => p.PasswordConfirmation;
                var attributes = property.GetAttributes<ResetPasswordForm, string, RemoteAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].HttpMethod.ShouldEqual("POST");
            }

            [TestMethod]
            public void IsDecoratedWith_Remote_UsingAdditionalFields_Password()
            {
                Expression<Func<ResetPasswordForm, string>> property = p => p.PasswordConfirmation;
                var attributes = property.GetAttributes<ResetPasswordForm, string, RemoteAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].AdditionalFields.ShouldEqual("Password");
            }
        }
    }
}
