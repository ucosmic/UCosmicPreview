using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ComponentSpace.SAML2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcContrib.TestHelper;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using System.Web;

namespace UCosmic.Www.Mvc.Areas.Identity.Controllers
{
    // ReSharper disable UnusedMember.Global
    public class Saml2ControllerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheComponentSpaceSignOnGetMethod
        {
            [TestMethod]
            public void IsUnitTestable()
            {
                var entityQueries = new Mock<IQueryEntities>();
                entityQueries.Setup(m => m.Establishments)
                    .Returns(new List<Establishment>
                    {
                        new Establishment
                        {
                            EmailDomains = new Collection<EstablishmentEmailDomain>
                            {
                                new EstablishmentEmailDomain
                                {
                                    Value = "@umn.edu",
                                }
                            },
                            SamlSignOn = new EstablishmentSamlSignOn
                            {
                                Id = 1,
                                EntityId = "https://idp.testshib.org/idp/shibboleth",
                                MetadataUrl = "https://idp.testshib.org/idp/shibboleth",
                            },
                        }
                    }.AsQueryable());
                //var samlCertificates = new PublicSamlCertificateStorage();
                var samlMetadata = new ComponentSpaceSaml2MetadataParser();
                var samlServiceProvider = new Mock<IProvideSaml2Service>();
                samlServiceProvider.Setup(m => m.SendAuthnRequest(It.IsAny<string>(), It.IsAny<Saml2SsoBinding>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<HttpContextBase>()));
                var http = new WebRequestHttpConsumer();
                var config = new DotNetConfigurationManager();
                var controller = new Saml2Controller(samlMetadata, http, config, samlServiceProvider.Object, entityQueries.Object);
                var builder = new TestControllerBuilder();
                builder.InitializeController(controller);
                SAML.HttpContext = controller.ControllerContext.HttpContext;
                //var result = controller.ComponentSpaceSignOn("someone@umn.edu", null);


            }
        }
    }
}
