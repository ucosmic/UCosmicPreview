using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Www.Mvc.Areas.Identity.Models
{
    // ReSharper disable UnusedMember.Global
    public class ConfirmEmailQueryProfilerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheQueryToFormProfile
        {
            [TestMethod]
            public void MapsToken()
            {
                var source = new ConfirmEmailQuery
                {
                    Token = Guid.NewGuid(),
                };

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.Token.ShouldEqual(source.Token);
            }

            [TestMethod]
            public void MapsSecretCode()
            {
                var source = new ConfirmEmailQuery
                {
                    SecretCode = "secret",
                };

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.SecretCode.ShouldEqual(source.SecretCode);
            }

            [TestMethod]
            public void MapsIntent()
            {
                var source = new ConfirmEmailQuery
                {
                    Intent = "intent",
                };

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.Intent.ShouldEqual(source.Intent);
            }

            [TestMethod]
            public void MapsIsUrlConfirmation_ToFalse_WhenSecretCode_IsNull()
            {
                var source = new ConfirmEmailQuery
                {
                    SecretCode = null,
                };

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.IsUrlConfirmation.ShouldBeFalse();
            }
                                                                    
            [TestMethod]                                            
            public void MapsIsUrlConfirmation_ToFalse_WhenSecretCode_IsEmptyString()
            {
                var source = new ConfirmEmailQuery
                {
                    SecretCode = string.Empty,
                };

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.IsUrlConfirmation.ShouldBeFalse();
            }
                                                                    
            [TestMethod]                                            
            public void MapsIsUrlConfirmation_ToFalse_WhenSecretCode_IsWhiteSpace()
            {
                var source = new ConfirmEmailQuery
                {
                    SecretCode = "\n",
                };

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.IsUrlConfirmation.ShouldBeFalse();
            }
                                                                    
            [TestMethod]                                            
            public void MapsIsUrlConfirmation_ToFalse_WhenSecretCode_IsNotNullWhiteSpace()
            {
                var source = new ConfirmEmailQuery
                {
                    SecretCode = "value",
                };

                var destination = Mapper.Map<ConfirmEmailForm>(source);

                destination.IsUrlConfirmation.ShouldBeTrue();
            }
        }
    }
}
