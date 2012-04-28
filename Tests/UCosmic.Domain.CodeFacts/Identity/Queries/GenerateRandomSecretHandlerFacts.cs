using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Identity
{
    // ReSharper disable UnusedMember.Global
    public class GenerateRandomSecretHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsArgumentNullException_WhenQueryArgIsNull()
            {
                var handler = new GenerateRandomSecretHandler();
                ArgumentNullException exception = null;
                try
                {
                    handler.Handle(null);
                }
                catch (ArgumentNullException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.ParamName.ShouldEqual("query");
                // ReSharper restore PossibleNullReferenceException
            }

            [TestMethod]
            public void GeneratesRandomSecret_OfExactLength()
            {
                const int length = 12;
                var query = new GenerateRandomSecretQuery(length);
                var handler = new GenerateRandomSecretHandler();

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.Length.ShouldEqual(length);
            }

            [TestMethod]
            public void GeneratesRandomSecret_OfVariableLength()
            {
                const int minLength = 44;
                const int maxLength = 792;
                var query = new GenerateRandomSecretQuery(minLength, maxLength);
                var handler = new GenerateRandomSecretHandler();

                var result = handler.Handle(query);

                result.ShouldNotBeNull();
                result.Length.ShouldBeInRange(minLength, maxLength);
            }
        }
    }
}
