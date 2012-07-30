using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Identity
{
    public static class GenerateRandomSecretQueryFacts
    {
        [TestClass]
        public class TheTwoArgConstructor
        {
            [TestMethod]
            public void ThrowsArgumentException_WhenMinimumLengthArg_IsLessThanOne()
            {
                ArgumentException exception = null;

                try
                {
                    new GenerateRandomSecretQuery(0, 5);
                }
                catch (ArgumentException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("minimumLength");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void ThrowsArgumentException_WhenMaximumLengthArg_IsLessThan_MinimumLengthArg()
            {
                ArgumentException exception = null;

                try
                {
                    new GenerateRandomSecretQuery(5, 4);
                }
                catch (ArgumentException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("maximumLength");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void SetsLengthProperties()
            {
                const int minLength = 41;
                const int maxLength = 112;

                var command = new GenerateRandomSecretQuery(minLength, maxLength);

                command.MinimumLength.ShouldEqual(minLength);
                command.MaximumLength.ShouldEqual(maxLength);
            }
        }

        [TestClass]
        public class TheOneArgConstructor
        {
            [TestMethod]
            public void ThrowsArgumentException_WhenArg_IsLessThanOne()
            {
                ArgumentException exception = null;

                try
                {
                    new GenerateRandomSecretQuery(0);
                }
                catch (ArgumentException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("minimumLength");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void SetsLengthProperties()
            {
                const int length = 57;

                var command = new GenerateRandomSecretQuery(length);

                command.MinimumLength.ShouldEqual(length);
                command.MaximumLength.ShouldEqual(length);
            }
        }
    }
}
