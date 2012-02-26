using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace UCosmic.Domain.Establishments
{
    // ReSharper disable UnusedMember.Global
    public class EstablishmentCategoryFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class EnglishNameProperty
        {
            [TestMethod]
            public void HasRequiredAttribute()
            {
                Expression<Func<EstablishmentCategory, string>> property = p => p.EnglishName;
                var attributes = property.GetAttributes<EstablishmentCategory, string, RequiredAttribute>();
                attributes.ShouldNotBeNull();
                attributes.Length.ShouldEqual(1);
                attributes[0].ShouldBeType<RequiredAttribute>();
            }
        }
    }
}
