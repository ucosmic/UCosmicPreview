using System;
using System.Collections.Generic;
using System.Linq;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Impl.Orm;

namespace UCosmic.Impl.Seeders
{
    public class InstitutionalAgreementConfigurationSeeder : ISeedDb
    {
        private static readonly IManageConfigurations WebConfig = new DotNetConfigurationManager();

        public void Seed(UCosmicContext context)
        {
            new InstitutionalAgreementConfigurationPreview4Seeder().Seed(context);
        }

        private class InstitutionalAgreementConfigurationPreview4Seeder : UCosmicDbSeeder
        {
            public override void Seed(UCosmicContext context)
            {
                if (WebConfig.IsDeployedToCloud) return;

                Context = context;

                Context.Set<InstitutionalAgreementConfiguration>().ToList().ForEach(c =>
                {
                    Context.Set<InstitutionalAgreementConfiguration>().Remove(c);
                    Context.SaveChanges();
                });

                var defaultTypes = new List<InstitutionalAgreementTypeValue>
                {
                    new InstitutionalAgreementTypeValue {Text = "Agreement Type #1"},
                    new InstitutionalAgreementTypeValue {Text = "Agreement Type #2"},
                    new InstitutionalAgreementTypeValue {Text = "Agreement Type #3"},
                };

                var defaultStatuses = new List<InstitutionalAgreementStatusValue>
                {
                    new InstitutionalAgreementStatusValue {Text = "Current Status #1"},
                    new InstitutionalAgreementStatusValue {Text = "Current Status #2"},
                    new InstitutionalAgreementStatusValue {Text = "Current Status #3"},
                    new InstitutionalAgreementStatusValue {Text = "Current Status #4"},
                };

                var defaultContactTypes = new List<InstitutionalAgreementContactTypeValue>
                {
                    new InstitutionalAgreementContactTypeValue {Text = "Contact Type #1"},
                    new InstitutionalAgreementContactTypeValue {Text = "Contact Type #2"},
                    new InstitutionalAgreementContactTypeValue {Text = "Contact Type #3"},
                    new InstitutionalAgreementContactTypeValue {Text = "Contact Type #4"},
                };

                var establishmentsToSeed = new[]
                {
                    "www.fue.edu.eg",
                    "www.griffith.edu.au",
                    "www.unsw.edu.au",
                };

                establishmentsToSeed.ToList().ForEach(w =>
                {
                    Context.Set<InstitutionalAgreementConfiguration>()
                        .Add(new InstitutionalAgreementConfiguration
                        {
                            ForEstablishment = Context.Set<Establishment>()
                            .SingleOrDefault(e => e.WebsiteUrl != null && e.WebsiteUrl.Equals(w, StringComparison.OrdinalIgnoreCase)),
                            AllowedTypeValues = defaultTypes.ToList(),
                            AllowedStatusValues = defaultStatuses.ToList(),
                            AllowedContactTypeValues = defaultContactTypes.ToList(),
                        });
                    Context.SaveChanges();
                });
            }
        }
    }
}