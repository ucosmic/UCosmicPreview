using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class UpdateNameFormFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheDisplayNameProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.DisplayName;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateNameForm.DisplayNameDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_Display_UsingPrompt()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.DisplayName;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Prompt.ShouldEqual(UpdateNameForm.DisplayNameDisplayPrompt);
            }
        }

        [TestClass]
        public class TheIsDisplayNameDerivedProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateNameForm, bool>> property = p => p.IsDisplayNameDerived;
                var attributes = property.GetAttributes<UpdateNameForm, bool, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateNameForm.IsDisplayNameDerivedDisplayName);
            }
        }

        [TestClass]
        public class TheSalutationProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.Salutation;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateNameForm.SalutationDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_DisplayFormat_UsingNullDisplayText()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.Salutation;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayFormatAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].NullDisplayText.ShouldEqual(UpdateNameForm.SalutationNullDisplayText);
            }
        }

        [TestClass]
        public class TheFirstNameProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.FirstName;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateNameForm.FirstNameDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_DisplayFormat_UsingNullDisplayText()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.FirstName;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayFormatAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].NullDisplayText.ShouldEqual(UpdateNameForm.FirstNameNullDisplayText);
            }
        }

        [TestClass]
        public class TheMiddleNameProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.MiddleName;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateNameForm.MiddleNameDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_DisplayFormat_UsingNullDisplayText()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.MiddleName;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayFormatAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].NullDisplayText.ShouldEqual(UpdateNameForm.MiddleNameNullDisplayText);
            }
        }

        [TestClass]
        public class TheLastNameProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.LastName;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateNameForm.LastNameDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_DisplayFormat_UsingNullDisplayText()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.LastName;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayFormatAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].NullDisplayText.ShouldEqual(UpdateNameForm.LastNameNullDisplayText);
            }
        }

        [TestClass]
        public class TheSuffixProperty
        {
            [TestMethod]
            public void IsDecoratedWith_Display_UsingName()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.Suffix;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].Name.ShouldEqual(UpdateNameForm.SuffixDisplayName);
            }

            [TestMethod]
            public void IsDecoratedWith_DisplayFormat_UsingNullDisplayText()
            {
                Expression<Func<UpdateNameForm, string>> property = p => p.Suffix;
                var attributes = property.GetAttributes<UpdateNameForm, string, DisplayFormatAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].NullDisplayText.ShouldEqual(UpdateNameForm.SuffixNullDisplayText);
            }
        }
    }
}
