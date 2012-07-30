using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Www.Mvc.Models
{
    public static class AutoMapperConfigFacts
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
