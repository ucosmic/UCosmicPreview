using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Domain
{
    public static class AutoMapperFacts
    {
        [TestClass]
        public class GeoNamesConverter
        {
            [TestMethod]
            public void ConfigurationIsValid()
            {
                Places.GeoNamesConverter.Configure();
                Mapper.AssertConfigurationIsValid();
            }
        }
    }
}
