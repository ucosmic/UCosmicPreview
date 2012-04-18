using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Saml
{
    // ReSharper disable UnusedMember.Global
    public class SamlAttributeFriendlyNameStringFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class AsSamlAttributeFriendlyNameMethod
        {
            [TestMethod]
            public void ReturnsEnumFor_EduPersonPrincipalNameString()
            {
                const string friendlyNameString = "eduPersonPrincipalName";
                var friendlyNameEnum = friendlyNameString.AsSamlAttributeFriendlyName();
                friendlyNameEnum.ShouldEqual(SamlAttributeFriendlyName.EduPersonPrincipalName);
            }

            [TestMethod]
            public void ThrowsInvalidOperationException_ForMisspelledEduPersonPrincipalNameString()
            {
                const string friendlyNameString = "EduPersonPrincipalName";
                InvalidOperationException exception = null;
                try
                {
                    var friendlyNameEnum = friendlyNameString.AsSamlAttributeFriendlyName();
                    friendlyNameEnum.ShouldBeNull();
                }
                catch (InvalidOperationException ex)
                {
                    exception = ex;
                }
                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ThrowsInvalidOperationException_ForNullString()
            {
                const string friendlyNameString = null;
                InvalidOperationException exception = null;
                try
                {
                    // ReSharper disable ExpressionIsAlwaysNull
                    var friendlyNameEnum = friendlyNameString.AsSamlAttributeFriendlyName();
                    // ReSharper restore ExpressionIsAlwaysNull
                    friendlyNameEnum.ShouldBeNull();
                }
                catch (InvalidOperationException ex)
                {
                    exception = ex;
                }
                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ThrowsInvalidOperationException_ForEmptyString()
            {
                var friendlyNameString = string.Empty;
                InvalidOperationException exception = null;
                try
                {
                    var friendlyNameEnum = friendlyNameString.AsSamlAttributeFriendlyName();
                    friendlyNameEnum.ShouldBeNull();
                }
                catch (InvalidOperationException ex)
                {
                    exception = ex;
                }
                exception.ShouldNotBeNull();
            }

            [TestMethod]
            public void ThrowsInvalidOperationException_ForWhiteSpaceString()
            {
                const string friendlyNameString = " \t\r\n ";
                InvalidOperationException exception = null;
                try
                {
                    var friendlyNameEnum = friendlyNameString.AsSamlAttributeFriendlyName();
                    friendlyNameEnum.ShouldBeNull();
                }
                catch (InvalidOperationException ex)
                {
                    exception = ex;
                }
                exception.ShouldNotBeNull();
            }
        }

        [TestClass]
        public class AsStringMethod
        {
            [TestMethod]
            public void ReturnsStringFor_EduPersonPrincipalNameEnum()
            {
                const SamlAttributeFriendlyName friendlyNameEnum = SamlAttributeFriendlyName.EduPersonPrincipalName;
                var friendlyNameString = friendlyNameEnum.AsString();
                friendlyNameString.ShouldEqual("eduPersonPrincipalName");
            }
        }
    }
}
