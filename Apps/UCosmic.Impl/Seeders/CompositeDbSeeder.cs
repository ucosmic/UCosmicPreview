using UCosmic.Domain;
using UCosmic.Domain.Establishments;
using UCosmic.Domain.InstitutionalAgreements;
using UCosmic.Impl.Orm;

namespace UCosmic.Impl.Seeders
{
    // The composite db seeder uses a combination of EF DbContext 
    // and raw SQL to seed a database. It runs much more slowly 
    // than the DevelopmentDataSqlSeeder. 
    // Typically you should use the composite db seeder only when
    // writing seed scripts. Once a composite seed script is run, 
    // you can then export the data as sql from SSMS and use it
    // to update the DevelopmentData.sql script. Just remember 
    // to remove the GO & print statements, and to disable /
    // restore reflexive foreign key constraints and the beginning
    // / end of the script, respectively. 


    // ReSharper disable ClassNeverInstantiated.Global
    public class CompositeDbSeeder : ISeedDb
    // ReSharper restore ClassNeverInstantiated.Global
    {
        public void Seed(UCosmicContext context)
        {
            //new LanguageSeeder().Seed(context);
            //new PlaceSeeder().Seed(context);
            new CoreDataSqlSeeder().Seed(context);
            new RoleSeeder().Seed(context);
            new EstablishmentSeeder().Seed(context);
            new RecruitmentAgencySeeder().Seed(context);
            new EmailTemplateSeeder().Seed(context);
            new PersonSeeder().Seed(context);
            new UserSeeder().Seed(context);
            new InstitutionalAgreementSeeder().Seed(context);
            new InstitutionalAgreementConfigurationSeeder().Seed(context);

            var commander = DependencyInjector.Current.GetService<ICommandObjects>();
            var queries = DependencyInjector.Current.GetService<IQueryEntities>();

            var updateEstablishmentHierarchy = DependencyInjector.Current
                .GetService<IHandleCommands<UpdateEstablishmentNodeHierarchyCommand>>();
            updateEstablishmentHierarchy.Handle(new UpdateEstablishmentNodeHierarchyCommand());
            commander.SaveChanges();

            var institutionalAgreementChanger = new InstitutionalAgreementChanger(commander, queries);
            institutionalAgreementChanger.DeriveNodes();
            commander.SaveChanges();
        }
    }
}