using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Establishments
{
    public static class InstitutionInfoFacts
    {
        [TestClass]
        public class CollegeBoardDesignatedIndicatorProperty
        {
            [TestMethod]
            public void HasGetSet()
            {
                const string value = "text";
                var entity = new InstitutionInfo { CollegeBoardDesignatedIndicator = value };
                entity.ShouldNotBeNull();
                entity.CollegeBoardDesignatedIndicator.ShouldEqual(value);
            }
        }
    }
}
