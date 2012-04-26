using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Impl.Orm;
using UCosmic.Www.Mvc.SpecFlow;

// ReSharper disable UnusedMember.Global
namespace UCosmic.Www.Mvc.Areas.InstitutionalAgreements.Events
{
    [Binding]
    public class InstitutionalAgreementEvents : TestRunEvents
    {
        [BeforeScenario("InstAgrFormsFreshTestAgreementUc01")]
        public static void FreshTestAgreementUc01()
        {
            using (var context = new UCosmicContext(null))
            {
                var entityId = new Guid("e8b53211-5b60-4b75-9c1a-e82f881a33a0");
                var agreement = context.InstitutionalAgreements.Current().SingleOrDefault(a => a.EntityId == entityId);
                if (agreement != null)
                {
                    context.InstitutionalAgreements.Remove(agreement);
                    context.SaveChanges();
                }
                var uc = context.Establishments.ByEmail("@uc.edu");
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
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufl.edu"), }, 
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufrj.br"), }, 
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufpr.br"), }, 
                    },
                };
                context.InstitutionalAgreements.Add(agreement);
                context.SaveChanges();
            }
        }

        [BeforeScenario("InstAgrFormsFreshTestAgreementUc02")]
        public static void FreshTestAgreementUc02()
        {
            using (var context = new UCosmicContext(null))
            {
                var entityId = new Guid("e8b53211-2222-2222-9c1a-e82f881a33a0");
                var agreement = context.InstitutionalAgreements.Current().SingleOrDefault(a => a.EntityId == entityId);
                if (agreement != null)
                {
                    context.InstitutionalAgreements.Remove(agreement);
                    context.SaveChanges();
                }
                var uc = context.Establishments.ByEmail("@uc.edu");
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
                context.InstitutionalAgreements.Add(agreement);
                context.SaveChanges();
            }
        }

        [BeforeScenario("InstAgrFormsFreshTestAgreementUcGc")]
        public static void FreshTestAgreementUcGc()
        {
            using (var context = new UCosmicContext(null))
            {
                var entityId = new Guid("ccb53211-5b60-4b75-9c1a-e82f881a33a0");
                var agreement = context.InstitutionalAgreements.Current().SingleOrDefault(a => a.EntityId == entityId);
                if (agreement != null)
                {
                    context.InstitutionalAgreements.Remove(agreement);
                    context.SaveChanges();
                }
                var uc = context.Establishments.ByEmail("@uc.edu");
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
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufl.edu"), }, 
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufrj.br"), }, 
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufpr.br"), }, 
                    },
                };
                context.InstitutionalAgreements.Add(agreement);
                context.SaveChanges();
            }
        }

        [BeforeScenario("InstAgrFormsFreshTestAgreementUcFf")]
        public static void FreshTestAgreementUcFf()
        {
            using (var context = new UCosmicContext(null))
            {
                var entityId = new Guid("ffb53211-5b60-4b75-9c1a-e82f881a33a0");
                var agreement = context.InstitutionalAgreements.Current().SingleOrDefault(a => a.EntityId == entityId);
                if (agreement != null)
                {
                    context.InstitutionalAgreements.Remove(agreement);
                    context.SaveChanges();
                }
                var uc = context.Establishments.ByEmail("@uc.edu");
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
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufl.edu"), }, 
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufrj.br"), }, 
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufpr.br"), }, 
                    },
                };
                context.InstitutionalAgreements.Add(agreement);
                context.SaveChanges();
            }
        }

        [BeforeScenario("InstAgrFormsFreshTestAgreementUcIe")]
        public static void FreshTestAgreementUcIe()
        {
            using (var context = new UCosmicContext(null))
            {
                var entityId = new Guid("eeb53211-5b60-4b75-9c1a-e82f881a33a0");
                var agreement = context.InstitutionalAgreements.Current().SingleOrDefault(a => a.EntityId == entityId);
                if (agreement != null)
                {
                    context.InstitutionalAgreements.Remove(agreement);
                    context.SaveChanges();
                }
                var uc = context.Establishments.ByEmail("@uc.edu");
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
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufl.edu"), }, 
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufrj.br"), }, 
                         new InstitutionalAgreementParticipant{ Establishment = context.Establishments.Single(e => e.WebsiteUrl == "www.ufpr.br"), }, 
                    },
                };
                context.InstitutionalAgreements.Add(agreement);
                context.SaveChanges();
            }
        }

        [BeforeTestRun]
        [BeforeScenario("InstAgrFormsResetTestAgreementsUc")]
        [AfterScenario("InstAgrFormsResetTestAgreementsUc")]
        public static void ResetTestAgreementsUc()
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
            using (var context = new UCosmicContext(null))
            {
                var agreements = context.InstitutionalAgreements.Where(a => 
                    titles.Contains(a.Title))
                    .ToList();
                if (agreements.Count <= 0) return;
                foreach (var agreement in agreements)
                    context.InstitutionalAgreements.Remove(agreement);
                context.SaveChanges();
            }
            FreshTestAgreementUc01();
        }

        [BeforeScenario("InstAgrConfigResetLehigh")]
        public static void ResetLehighData()
        {
            ResetConfigurationData("www.lehigh.edu");
        }

        [BeforeScenario("InstAgrConfigResetUmn")]
        public static void ResetUmnData()
        {
            ResetConfigurationData("www.umn.edu");
        }

        [BeforeScenario("InstAgrConfigResetUmn")]
        public static void ResetUsilData()
        {
            ResetConfigurationData("www.usil.edu.pe");
        }

        [BeforeScenario("InstAgrConfigResetEdinburgh")]
        public static void ResetEdinburghData()
        {
            ResetConfigurationData("www.napier.ac.uk");
        }

        [BeforeScenario("InstAgrConfigResetSuny")]
        public static void ResetSunyData()
        {
            ResetConfigurationData("www.suny.edu");
        }

        [BeforeScenario("InstAgrConfigResetBjtu")]
        public static void ResetBjtuData()
        {
            ResetConfigurationData("www.bjtu.edu.cn");
        }

        private static void ResetConfigurationData(string url)
        {
            using (var context = new UCosmicContext(null))
            {
                var config = context.InstitutionalAgreementConfigurations.SingleOrDefault(c =>
                    url.Equals(c.ForEstablishment.WebsiteUrl));
                if (config == null) return;
                context.InstitutionalAgreementConfigurations.Remove(config);
                context.SaveChanges();
            }
        }
    }
}
// ReSharper restore UnusedMember.Global
