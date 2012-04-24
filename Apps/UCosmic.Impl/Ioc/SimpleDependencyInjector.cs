using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using FluentValidation;
using NGeo.GeoNames;
using NGeo.Yahoo.GeoPlanet;
using NGeo.Yahoo.PlaceFinder;
using SimpleInjector;
using SimpleInjector.Extensions;
using UCosmic.Domain;
using UCosmic.Orm;
using UCosmic.Seeders;
using UCosmic.Domain.Email;

namespace UCosmic
{
    public class SimpleDependencyInjector : IServiceProvider
    {
        private readonly Container _container;

        public SimpleDependencyInjector()
        {
            _container = Bootstrap();
        }

        internal static Container Bootstrap()
        {
            var container = new Container();

            /**
             * Entity Framework Dependency Injection:
             * 
             * There are 2 main dependencies: database initialization, and database seeding.
             * Whenever the domain entity model changes, the SQL db will be out of sync.
             * The DropOnModelChangeInitializer only drops and recreates the database when the entity model changes.
             * The DropAlwaysInitializer drops and recreates the database after each new solution rebuild.
             * The BrownfieldInitializer never drops the database, even if the entity model does not match.
             * However the initializer only drops and recreates the database, all tables will be empty.
             * To populate with new data, use the CompositeDbSeeder.
             * It uses a combination of DbContext and raw SQL to populate the database.
             * When the BrownfieldDbSeeder is injected, no data will be seeded.
             * 
             * 2012.02.22: There is now a DevelopmentDataSqlSeeder, which is much faster than the CompositeDbSeeder.
             * 
             * When checking in this file, the DropOnModelChangeInitializer and DevelopmentDataSqlSeeder
             * should be active. All other seeders and initializers should be commented out.
             */
            container.Register<IDatabaseInitializer<UCosmicContext>, DropOnModelChangeInitializer>();
            //container.Register<IDatabaseInitializer<UCosmicContext>, DropAlwaysInitializer>();
            //container.Register<IDatabaseInitializer<UCosmicContext>, BrownfieldInitializer>();

            container.Register<ISeedDb, DevelopmentDataSqlSeeder>();
            //container.Register<ISeedDb, CompositeDbSeeder>();
            //container.Register<ISeedDb, BrownfieldDbSeeder>();

            // register 1 DbContext for all implemented interfaces
            container.RegisterPerWebRequest<UCosmicContext>();
            container.Register<IUnitOfWork>(container.GetInstance<UCosmicContext>);
            container.Register<IQueryEntities>(container.GetInstance<UCosmicContext>);
            container.Register<ICommandEntities>(container.GetInstance<UCosmicContext>);

            // other interfaces related to DbContext
            container.Register<ICommandObjects, ObjectCommander>();
            container.RegisterPerWebRequest<RoleProvider, AuthorizationProvider>();

            // general purpose interfaces
            container.Register<ISignMembers, DotNetMembershipProvider>();
            container.Register<ISignUsers, DotNetFormsAuthentication>();
            container.Register<IManageConfigurations, DotNetConfigurationManager>();
            container.Register<ILogExceptions, ElmahExceptionLogger>();
            container.Register<IConsumeHttp, WebRequestHttpConsumer>();
            container.Register<ISendEmails, MvcEmailSender>();
            container.Register<ISendMail, SmtpMailSender>();

            // SAML interfaces
            container.Register<IProvideSaml2Service, ComponentSpaceSaml2ServiceProvider>();
            container.Register<IParseSaml2Metadata, ComponentSpaceSaml2MetadataParser>();
            container.Register<IStoreSamlCertificates, PublicSamlCertificateStorage>();
            //container.Register<IStoreSamlCertificates, PrivateSamlCertificateStorage>();

            // NGeo interfaces
            container.RegisterPerWebRequest<IConsumeGeoNames, GeoNamesClient>();
            container.RegisterPerWebRequest<IConsumeGeoPlanet, GeoPlanetClient>();
            container.RegisterPerWebRequest<IConsumePlaceFinder, PlaceFinderClient>();

            // load assemblies for IoC reflection
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var msWebMvc = assemblies.SingleOrDefault(a => a.FullName.StartsWith("Microsoft.Web.Mvc"));
            if (msWebMvc != null) assemblies.Remove(msWebMvc);

            // fluent validation open generics
            container.RegisterManyForOpenGeneric(typeof(IValidator<>), assemblies);

            // open generic decorator chains http://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=91
            container.RegisterManyForOpenGeneric(typeof(IHandleCommands<>), (type, implementations) =>
                {
                    // register the async email handler
                    if (type == typeof(IHandleCommands<SendEmailMessageCommand>))
                        container.Register(type, implementations
                            .Single(i => i == typeof(SendAsyncEmailMessageHandler)));

                    else if (implementations.Length < 1)
                        throw new InvalidOperationException(string.Format(
                            "No implementations were found for type '{0}'.",
                                type.Name));
                    else if (implementations.Length > 1)
                        throw new InvalidOperationException(string.Format(
                            "{1} implementations were found for type '{0}'.",
                                type.Name, implementations.Length));

                    // register a single implementation (default behavior)
                    else
                        container.Register(type, implementations.Single());

                }, assemblies);
            container.RegisterOpenGenericDecorator(typeof(IHandleCommands<>),
                typeof(FluentValidationCommandDecorator<>));

            // query processing
            container.RegisterPerWebRequest<SimpleQueryProcessor>();
            container.Register<IProcessQueries>(container.GetInstance<SimpleQueryProcessor>);
            container.RegisterManyForOpenGeneric(typeof(IHandleQueries<,>), assemblies);

            // verify container
            container.Verify();

            return container;
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container).GetService(serviceType);
        }
    }
}
