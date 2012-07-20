using System;
using System.Collections.Generic;
using System.Linq;
using ServiceLocatorPattern;
using TechTalk.SpecFlow;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;

namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements
{
    [Binding]
    public class ManageEvents : BaseStepDefinition
    {
        [BeforeTestRun]
        [AfterTestRun]
        [BeforeScenario("UsingFreshExampleUcInstitutionalAgreementData")]
        public static void ResetExampleUcInstitutionalAgreementData()
        {
            RemoveExampleUcInstitutionalAgreementsCreatedByTests();
            ResetExampleUcTestInstitutionalAgreement1();
            ResetExampleUcTestInstitutionalAgreement2();
            ResetExampleUcTestInstitutionalAgreementForGoogleChrome();
            ResetExampleUcTestInstitutionalAgreementForFirefox();
            ResetExampleUcTestInstitutionalAgreementForMsie();
        }

        private static void ResetExampleUcTestInstitutionalAgreement1()
        {
            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var entityId = new Guid("e8b53211-5b60-4b75-9c1a-e82f881a33a0");
            var agreement = entities.Get<InstitutionalAgreement>().SingleOrDefault(a => a.EntityId == entityId);
            if (agreement != null)
            {
                entities.Purge(agreement);
                unitOfWork.SaveChanges();
            }
            var uc = entities.Get<Establishment>().ByEmail("@uc.edu");
            agreement = new InstitutionalAgreement
            {
                EntityId = entityId,
                Type = "Activity Agreement",
                Title = "Agreement, UC 01 test",
                StartsOn = DateTime.Parse("8/1/2009"),
                ExpiresOn = DateTime.Parse("7/31/2014"),
                Status = "Active",
                IsTitleDerived = false,
                IsAutoRenew = false,
                Description = "This multilateral agreement is between four (4) institutions: the University of Cincinnati (U.S. lead institution), the University of Florida, the Universidade Federal do Rio de Janeiro (Brazilian lead institution), and the Universidade Federal do Parana. \r\n\r\nAgreement is for undergraduate and graduate student exchange. \r\n\r\nThis agreement actually contains two different end dates: Page 1, Article 4, cites a four-year span, ending July 31, 2013. Page 5, Article 22, says it is a 5-year span.",
                Participants = new List<InstitutionalAgreementParticipant>
                {
                    new InstitutionalAgreementParticipant{ Establishment = uc, IsOwner = true, },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufl.edu"), },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufrj.br"), },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufpr.br"), },
                },
            };
            entities.Create(agreement);
            unitOfWork.SaveChanges();
        }

        private static void ResetExampleUcTestInstitutionalAgreement2()
        {
            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var entityId = new Guid("e8b53211-2222-2222-9c1a-e82f881a33a0");
            var agreement = entities.Get<InstitutionalAgreement>().SingleOrDefault(a => a.EntityId == entityId);
            if (agreement != null)
            {
                entities.Purge(agreement);
                unitOfWork.SaveChanges();
            }
            var uc = entities.Get<Establishment>().ByEmail("@uc.edu");
            agreement = new InstitutionalAgreement
            {
                EntityId = entityId,
                Type = "Memorandum of Understanding",
                Title = "Agreement, UC 02 test",
                StartsOn = DateTime.Parse("9/2/2009"),
                ExpiresOn = DateTime.Parse("9/1/2014"),
                Status = "Active",
                IsTitleDerived = false,
                IsAutoRenew = false,
                Description = "This multilateral agreement is between four (4) institutions: the University of Cincinnati (U.S. lead institution), the University of Florida, the Universidade Federal do Rio de Janeiro (Brazilian lead institution), and the Universidade Federal do Parana. \r\n\r\nAgreement is for undergraduate and graduate student exchange. \r\n\r\nThis agreement actually contains two different end dates: Page 1, Article 4, cites a four-year span, ending July 31, 2013. Page 5, Article 22, says it is a 5-year span.",
                Participants = new List<InstitutionalAgreementParticipant>
                {
                    new InstitutionalAgreementParticipant{ Establishment = uc, IsOwner = true, },
                },
            };
            entities.Create(agreement);
            unitOfWork.SaveChanges();
        }

        private static void ResetExampleUcTestInstitutionalAgreementForGoogleChrome()
        {
            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var entityId = new Guid("ccb53211-5b60-4b75-9c1a-e82f881a33a0");
            var agreement = entities.Get<InstitutionalAgreement>().SingleOrDefault(a => a.EntityId == entityId);
            if (agreement != null)
            {
                entities.Purge(agreement);
                unitOfWork.SaveChanges();
            }
            var uc = entities.Get<Establishment>().ByEmail("@uc.edu");
            agreement = new InstitutionalAgreement
            {
                EntityId = entityId,
                Type = "Activity Agreement",
                Title = "Agreement, UC GC test",
                StartsOn = DateTime.Parse("8/1/2009"),
                ExpiresOn = DateTime.Parse("7/31/2014"),
                Status = "Active",
                IsTitleDerived = false,
                IsAutoRenew = false,
                Description = "This multilateral agreement is between four (4) institutions: the University of Cincinnati (U.S. lead institution), the University of Florida, the Universidade Federal do Rio de Janeiro (Brazilian lead institution), and the Universidade Federal do Parana. \r\n\r\nAgreement is for undergraduate and graduate student exchange. \r\n\r\nThis agreement actually contains two different end dates: Page 1, Article 4, cites a four-year span, ending July 31, 2013. Page 5, Article 22, says it is a 5-year span.",
                Participants = new List<InstitutionalAgreementParticipant>
                {
                    new InstitutionalAgreementParticipant{ Establishment = uc, IsOwner = true, },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufl.edu"), },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufrj.br"), },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufpr.br"), },
                },
            };
            entities.Create(agreement);
            unitOfWork.SaveChanges();
        }

        private static void ResetExampleUcTestInstitutionalAgreementForFirefox()
        {
            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var entityId = new Guid("ffb53211-5b60-4b75-9c1a-e82f881a33a0");
            var agreement = entities.Get<InstitutionalAgreement>().SingleOrDefault(a => a.EntityId == entityId);
            if (agreement != null)
            {
                entities.Purge(agreement);
                unitOfWork.SaveChanges();
            }
            var uc = entities.Get<Establishment>().ByEmail("@uc.edu");
            agreement = new InstitutionalAgreement
            {
                EntityId = entityId,
                Type = "Activity Agreement",
                Title = "Agreement, UC FF test",
                StartsOn = DateTime.Parse("8/1/2009"),
                ExpiresOn = DateTime.Parse("7/31/2014"),
                Status = "Active",
                IsTitleDerived = false,
                IsAutoRenew = false,
                Description = "This multilateral agreement is between four (4) institutions: the University of Cincinnati (U.S. lead institution), the University of Florida, the Universidade Federal do Rio de Janeiro (Brazilian lead institution), and the Universidade Federal do Parana. \r\n\r\nAgreement is for undergraduate and graduate student exchange. \r\n\r\nThis agreement actually contains two different end dates: Page 1, Article 4, cites a four-year span, ending July 31, 2013. Page 5, Article 22, says it is a 5-year span.",
                Participants = new List<InstitutionalAgreementParticipant>
                {
                    new InstitutionalAgreementParticipant{ Establishment = uc, IsOwner = true, },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufl.edu"), },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufrj.br"), },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufpr.br"), },
                },
            };
            entities.Create(agreement);
            unitOfWork.SaveChanges();
        }

        private static void ResetExampleUcTestInstitutionalAgreementForMsie()
        {
            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var entityId = new Guid("eeb53211-5b60-4b75-9c1a-e82f881a33a0");
            var agreement = entities.Get<InstitutionalAgreement>().SingleOrDefault(a => a.EntityId == entityId);
            if (agreement != null)
            {
                entities.Purge(agreement);
                unitOfWork.SaveChanges();
            }
            var uc = entities.Get<Establishment>().ByEmail("@uc.edu");
            agreement = new InstitutionalAgreement
            {
                EntityId = entityId,
                Type = "Activity Agreement",
                Title = "Agreement, UC IE test",
                StartsOn = DateTime.Parse("8/1/2009"),
                ExpiresOn = DateTime.Parse("7/31/2014"),
                Status = "Active",
                IsTitleDerived = false,
                IsAutoRenew = false,
                Description = "This multilateral agreement is between four (4) institutions: the University of Cincinnati (U.S. lead institution), the University of Florida, the Universidade Federal do Rio de Janeiro (Brazilian lead institution), and the Universidade Federal do Parana. \r\n\r\nAgreement is for undergraduate and graduate student exchange. \r\n\r\nThis agreement actually contains two different end dates: Page 1, Article 4, cites a four-year span, ending July 31, 2013. Page 5, Article 22, says it is a 5-year span.",
                Participants = new List<InstitutionalAgreementParticipant>
                {
                    new InstitutionalAgreementParticipant{ Establishment = uc, IsOwner = true, },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufl.edu"), },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufrj.br"), },
                    new InstitutionalAgreementParticipant{ Establishment = entities.Get<Establishment>().Single(e => e.WebsiteUrl == "www.ufpr.br"), },
                },
            };
            entities.Create(agreement);
            unitOfWork.SaveChanges();
        }

        private static void RemoveExampleUcInstitutionalAgreementsCreatedByTests()
        {
            var titles = new[]
            {
                "Agreement, UC A1 test",
                "Agreement, UC B1 test",
                "Agreement, UC A2 test",
                "Agreement, UC B2 test",
                "Agreement, UC GC test",
                "Agreement, UC FF test",
                "Agreement, UC IE test",
                "1 Agreement Test R0315A",
                "1 Agreement Test R0315B",
                "1 Agreement Test R0316A",
                "1 Agreement Test R0316B",
                "Agreement, UC B test",
            };

            var entities = ServiceProviderLocator.Current.GetService<ICommandEntities>();
            var unitOfWork = ServiceProviderLocator.Current.GetService<IUnitOfWork>();

            var agreements = entities.Get<InstitutionalAgreement>().Where(a =>
                titles.Contains(a.Title))
                .ToList();
            if (agreements.Count <= 0) return;
            foreach (var agreement in agreements)
                entities.Purge(agreement);
            unitOfWork.SaveChanges();
            //FreshTestAgreementUc01();
        }
    }
}
