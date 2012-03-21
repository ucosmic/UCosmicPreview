using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Www.Mvc.Models
{
    // ReSharper disable UnusedMember.Global
    public class AutoMapperConfigFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class ConfigureMethod
        {
            [TestMethod]
            public void ConfigurationIsValid()
            {
                Mapper.AssertConfigurationIsValid();
            }
        }
    }
}
