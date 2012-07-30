using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using UCosmic.Impl;

namespace UCosmic.Www.Mvc
{
    public static class SimpleDependencyInjectorFacts
    {
        [TestClass]
        public class TheBootstrapMethod
        {
            [TestMethod]
            public void Returns_Verifyable_Container()
            {
                var container = SimpleDependencyInjector.Bootstrap(new ContainerConfiguration());
                container.Verify();
            }

            [TestMethod]
            public void Registers_UCosmicContext_PerWebRequest()
            {
                var request = new HttpRequest(null, "http://www.site.com", null);
                HttpContext.Current = new HttpContext(request, new HttpResponse(null));

                var container = SimpleDependencyInjector.Bootstrap(new ContainerConfiguration());

                var db1 = container.GetInstance<IUnitOfWork>();
                var db2 = container.GetInstance<IQueryEntities>();
                var db3 = container.GetInstance<ICommandEntities>();

                db1.ShouldNotBeNull();
                db2.ShouldNotBeNull();
                db3.ShouldNotBeNull();

                db1.ShouldEqual(db2 as IUnitOfWork);
                db2.ShouldEqual(db3);
                db3.ShouldEqual(db1 as ICommandEntities);
            }
        }
    }
}
