using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using Should;
using UCosmic.Domain.Identity;
using UCosmic.Domain.People;

namespace UCosmic.Domain.Establishments
{
    // ReSharper disable UnusedMember.Global
    public class SignOnSamlUserHandlerFacts
    // ReSharper restore UnusedMember.Global
    {
        [TestClass]
        public class TheHandleMethod
        {
            [TestMethod]
            public void ThrowsInvalidOperationException_WhenEstablishmentIsNotTrusted()
            {
                var scenarioOptions = new ScenarioInfo
                {
                    NullIssuingEstablishment = true,
                    IssuerNameIdentifier = "https://untrusted.idp.tld/idp/shibboleth"
                };
                var handler = CreateSignOnSamlUserHandler(scenarioOptions);
                InvalidOperationException exception = null;
                try
                {
                    handler.Handle(CreateSignOnSamlUserCommand());
                }
                catch (InvalidOperationException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.Message.ShouldEqual(string.Format(
                    SignOnSamlUserHandler.UntrustedIssuerExceptionMessageFormat, 
                        scenarioOptions.IssuerNameIdentifier));
                // ReSharper restore PossibleNullReferenceException
                scenarioOptions.MockQueryProcessor.Verify(m => 
                    m.Execute(It.IsAny<GetEstablishmentBySamlEntityIdQuery>()), 
                        Times.Once());
            }

            [TestMethod]
            public void ThrowsInvalidOperationException_WhenSamlResponseSignatureFailsVerification()
            {
                var scenarioOptions = new ScenarioInfo
                {
                    FailResponseSignatureVerification = true,
                };
                var handler = CreateSignOnSamlUserHandler(scenarioOptions);
                InvalidOperationException exception = null;
                try
                {
                    handler.Handle(CreateSignOnSamlUserCommand());
                }
                catch (InvalidOperationException ex)
                {
                    exception = ex;
                }

                exception.ShouldNotBeNull();
                // ReSharper disable PossibleNullReferenceException
                exception.Message.ShouldEqual(string.Format(
                    SignOnSamlUserHandler.ResponseSignatureFailedVerificationMessageFormat,
                        scenarioOptions.IssuerNameIdentifier));
                // ReSharper restore PossibleNullReferenceException
                scenarioOptions.MockQueryProcessor.Verify(m => 
                    m.Execute(It.IsAny<GetEstablishmentBySamlEntityIdQuery>()), 
                        Times.Once());
            }

            [TestMethod]
            public void SearchesForUser_ByEduPersonTargetedId()
            {
                var scenarioOptions = new ScenarioInfo();
                var handler = CreateSignOnSamlUserHandler(scenarioOptions);

                handler.Handle(CreateSignOnSamlUserCommand());

                scenarioOptions.MockQueryProcessor.Verify(m => 
                    m.Execute(It.IsAny<GetUserByEduPersonTargetedIdQuery>()), 
                        Times.Once());
            }

            [TestMethod]
            public void SignsOnUser_WhenEduPersonTargetedId_IsFound()
            {
                var scenarioOptions = new ScenarioInfo();
                var handler = CreateSignOnSamlUserHandler(scenarioOptions);

                handler.Handle(CreateSignOnSamlUserCommand());

                scenarioOptions.MockUserSigner.Verify(m => 
                    m.SignOn(scenarioOptions.EduPersonPrincipalName, false, null), 
                        Times.Once());
            }

            [TestMethod]
            public void LogsSubjectNameIdentifier_WhenEduPersonTargetedId_IsFound()
            {
                var scenarioOptions = new ScenarioInfo();
                var handler = CreateSignOnSamlUserHandler(scenarioOptions);

                handler.Handle(CreateSignOnSamlUserCommand());

                scenarioOptions.UserWithEduPersonTargetedId.ShouldNotBeNull();
                scenarioOptions.UserWithEduPersonTargetedId.SubjectNameIdentifiers.ShouldNotBeNull();
                scenarioOptions.UserWithEduPersonTargetedId.SubjectNameIdentifiers.Count.ShouldEqual(1);
                scenarioOptions.UserWithEduPersonTargetedId.SubjectNameIdentifiers.Single()
                    .Value.ShouldEqual(scenarioOptions.SubjectNameIdentifier);
                scenarioOptions.UserWithEduPersonTargetedId.SubjectNameIdentifiers.Single()
                    .UpdatedOnUtc.ShouldBeNull();
                scenarioOptions.UserWithEduPersonTargetedId.SubjectNameIdentifiers.Single()
                    .Number.ShouldEqual(1);
            }

            [TestMethod]
            public void UpdatesSubjectNameIdentifier_WhenEduPersonTargetedId_IsFound()
            {
                var scenarioOptions = new ScenarioInfo
                {
                    UserWithEduPersonTargetedIdAlreadyHasSubjectNameIdentifier = true,
                };
                var handler = CreateSignOnSamlUserHandler(scenarioOptions);

                handler.Handle(CreateSignOnSamlUserCommand());

                scenarioOptions.UserWithEduPersonTargetedId.ShouldNotBeNull();
                scenarioOptions.UserWithEduPersonTargetedId.SubjectNameIdentifiers.ShouldNotBeNull();
                scenarioOptions.UserWithEduPersonTargetedId.SubjectNameIdentifiers.Count.ShouldEqual(1);
                scenarioOptions.UserWithEduPersonTargetedId.SubjectNameIdentifiers.Single()
                    .Value.ShouldEqual(scenarioOptions.SubjectNameIdentifier);
                scenarioOptions.UserWithEduPersonTargetedId.SubjectNameIdentifiers.Single()
                    .UpdatedOnUtc.ShouldNotEqual(null);
            }
        }

        private class ScenarioInfo
        {
            internal bool NullIssuingEstablishment;

            internal bool NullUserWithEduPersonTargetedId;
            internal User UserWithEduPersonTargetedId;
            internal bool UserWithEduPersonTargetedIdAlreadyHasSubjectNameIdentifier;

            internal string IssuerNameIdentifier = TestShibIssuerNameIdentifier;
            internal const string TestShibIssuerNameIdentifier = "https://idp.testshib.org/idp/shibboleth";
            internal const string UcQaIssuerNameIdentifier = "https://qalogin.uc.edu/idp/shibboleth";

            internal string SubjectNameIdentifier = SampleSubjectNameIdentifier1;
            internal const string SampleSubjectNameIdentifier1 = "_871132b65b96f1df7fe5344757e7bb79";

            internal string EduPersonPrincipalName = MyselfAtTestShibEduPersonPrincipalName;
            internal const string MyselfAtTestShibEduPersonPrincipalName = "myself@testshib.org";

            internal bool FailResponseSignatureVerification;

            internal Mock<IProcessQueries> MockQueryProcessor { get; set; }
            internal Mock<ISignUsers> MockUserSigner { get; set; }

            public string EduPersonTargetedId { get; set; }

            public string[] EduPersonScopedAffiliations { get; set; }

            public string[] Mails { get; set; }
        }

        private static SignOnSamlUserHandler CreateSignOnSamlUserHandler(ScenarioInfo scenarioInfo)
        {
            scenarioInfo = scenarioInfo ?? new ScenarioInfo();

            #region IProcessQueries

            scenarioInfo.MockQueryProcessor = new Mock<IProcessQueries>(MockBehavior.Strict);

            // return a mock Saml2Response
            var samlResponse = new Mock<Saml2Response>(MockBehavior.Strict);
            samlResponse.Setup(p => p.IssuerNameIdentifier).Returns(scenarioInfo.IssuerNameIdentifier);
            samlResponse.Setup(p => p.SubjectNameIdentifier).Returns(scenarioInfo.SubjectNameIdentifier);
            samlResponse.Setup(m => m.VerifySignature()).Returns(!scenarioInfo.FailResponseSignatureVerification);
            samlResponse.Setup(p => p.EduPersonPrincipalName).Returns(scenarioInfo.EduPersonPrincipalName);
            samlResponse.Setup(p => p.EduPersonTargetedId).Returns(scenarioInfo.EduPersonTargetedId);
            samlResponse.Setup(p => p.EduPersonScopedAffiliations).Returns(scenarioInfo.EduPersonScopedAffiliations);
            samlResponse.Setup(p => p.Mails).Returns(scenarioInfo.Mails);
            scenarioInfo.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<ReceiveSaml2ResponseQuery>()))
                .Returns(samlResponse.Object);

            // return the issuing establishment
            Establishment issuingEstablishment = null;
            if (!scenarioInfo.NullIssuingEstablishment)
                issuingEstablishment = new Establishment
                {
                    SamlSignOn = new EstablishmentSamlSignOn
                    {
                        EntityId = scenarioInfo.IssuerNameIdentifier,
                    }
                };
            scenarioInfo.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<GetEstablishmentBySamlEntityIdQuery>()))
                .Returns(issuingEstablishment);

            // return the edu person targeted id
            if (!scenarioInfo.NullUserWithEduPersonTargetedId)
                scenarioInfo.UserWithEduPersonTargetedId = new User
                {
                    Person = new Person(),
                };

            // set up previous subject name identifier
            if (scenarioInfo.UserWithEduPersonTargetedId != null &&
                scenarioInfo.UserWithEduPersonTargetedIdAlreadyHasSubjectNameIdentifier)
                scenarioInfo.UserWithEduPersonTargetedId.SubjectNameIdentifiers =
                    new Collection<SubjectNameIdentifier>
                    {
                        new SubjectNameIdentifier
                        {
                            Number = 1,
                            Value = scenarioInfo.SubjectNameIdentifier,
                            CreatedOnUtc = DateTime.UtcNow.AddHours(-1),
                        }
                    };
            scenarioInfo.MockQueryProcessor.Setup(m => m.Execute(It.IsAny<GetUserByEduPersonTargetedIdQuery>()))
                .Returns(scenarioInfo.UserWithEduPersonTargetedId);

            #endregion
            #region ISignUsers

            scenarioInfo.MockUserSigner = new Mock<ISignUsers>(MockBehavior.Strict);
            scenarioInfo.MockUserSigner.Setup(m => m.SignOn(It.IsAny<string>(), false, null));

            #endregion

            return new SignOnSamlUserHandler(scenarioInfo.MockQueryProcessor.Object, 
                null, scenarioInfo.MockUserSigner.Object);
        }

        private static SignOnSamlUserCommand CreateSignOnSamlUserCommand()
        {
            var command = new SignOnSamlUserCommand
            {
                SsoBinding = Saml2SsoBinding.HttpPost,
                HttpContext = new Mock<HttpContextBase>(MockBehavior.Strict).Object,
            };
            return command;
        }
    }
}
