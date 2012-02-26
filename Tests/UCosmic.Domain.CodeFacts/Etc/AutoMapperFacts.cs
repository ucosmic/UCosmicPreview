using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UCosmic.Domain
{
    // ReSharper disable UnusedMember.Global
    public class AutoMapperFacts
    // ReSharper restore UnusedMember.Global
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
