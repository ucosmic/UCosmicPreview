using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using FluentValidation;
using NGeo.GeoNames;
using NGeo.Yahoo.GeoPlanet;
using NGeo.Yahoo.PlaceFinder;
using SimpleInjector;
using SimpleInjector.Extensions;
using UCosmic.Domain.Identity;
using UCosmic.Impl.BinaryData;
using UCosmic.Impl.Orm;
using UCosmic.Impl.Seeders;

namespace UCosmic.Impl
{
    public class ContainerConfiguration
    {
        public bool IsDeployedToCloud { get; set; }
        public string GeoPlanetAppId { get; set; }
        public string GeoNamesUserName { get; set; }
    }

    public class SimpleDependencyInjector : IServiceProvider
    {
        private readonly Container _container;

        public SimpleDependencyInjector(ContainerConfiguration configuration)
        {
            _container = Bootstrap(configuration);
        }

        internal static Container Bootstrap(ContainerConfiguration configuration)
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
            if (configuration.IsDeployedToCloud)
            {
                container.Register<IDatabaseInitializer<UCosmicContext>, BrownfieldInitializer>();
                //container.Register<ISeedData, BrownfieldSeeder>();
            }
            else
            {
                //container.Register<IDatabaseInitializer<UCosmicContext>, DropOnModelChangeInitializer>();
                //container.Register<IDatabaseInitializer<UCosmicContext>, DropAlwaysInitializer>();
                container.Register<IDatabaseInitializer<UCosmicContext>, BrownfieldInitializer>();

                //container.Register<ISeedData, CompositeSqlSeeder>();
                //container.Register<ISeedData, CompositeEntitySeeder>();
                container.Register<ISeedData, BrownfieldSeeder>();
            }

            // register 1 DbContext for all implemented interfaces
            container.RegisterPerWebRequest<UCosmicContext>();
            container.Register<IUnitOfWork>(container.GetInstance<UCosmicContext>);
            container.Register<IQueryEntities>(container.GetInstance<UCosmicContext>);
            container.Register<ICommandEntities>(container.GetInstance<UCosmicContext>);
            container.RegisterInitializer<UCosmicContext>(container.InjectProperties);

            // other interfaces related to DbContext
            //container.Register<ICommandObjects, ObjectCommander>();

            // general purpose interfaces
            container.Register<IStorePasswords, DotNetMembershipProvider>();
            container.Register<ISignUsers, DotNetFormsAuthentication>();
            container.Register<IManageConfigurations, DotNetConfigurationManager>();
            container.Register<ILogExceptions, ElmahExceptionLogger>();
            container.Register<IConsumeHttp, WebRequestHttpConsumer>();
            container.Register<ISendMail, SmtpMailSender>();

            // SAML interfaces
            container.Register<IProvideSaml2Service, ComponentSpaceSaml2ServiceProvider>();
            container.Register<IParseSaml2Metadata, ComponentSpaceSaml2MetadataParser>();
            container.Register<IStoreSamlCertificates, RealSamlCertificateStorage>();

            // NGeo interfaces
            container.RegisterPerWebRequest<IConsumeGeoNames, GeoNamesClient>();
            container.RegisterPerWebRequest<IContainGeoNames>(() => new GeoNamesContainer(configuration.GeoNamesUserName));
            container.RegisterPerWebRequest<IConsumeGeoPlanet, GeoPlanetClient>();
            container.RegisterPerWebRequest<IContainGeoPlanet>(() => new GeoPlanetContainer(configuration.GeoPlanetAppId));
            container.RegisterPerWebRequest<IConsumePlaceFinder, PlaceFinderClient>();

            // load assemblies for IoC reflection
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.FullName.StartsWith("Microsoft.Web.Mvc,"))
                .ToArray();

            // fluent validation open generics
            container.RegisterManyForOpenGeneric(typeof(IValidator<>), assemblies);

            // add unregistered type resolution for objects missing an IValidator<T>
            container.RegisterSingleOpenGeneric(typeof(IValidator<>), typeof(UnspecifiedValidator<>));

            // open generic decorator chains http://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=91
            container.RegisterManyForOpenGeneric(typeof(IHandleCommands<>), assemblies);

            // send emails in a new thread
            container.RegisterRunAsyncCommandHandlerProxy<SendEmailMessageCommand>();

            // register fluent validators on commands
            container.RegisterDecorator(typeof(IHandleCommands<>),
                typeof(FluentValidationCommandDecorator<>));
            //container.RegisterOpenGenericDecorator(typeof(IHandleCommands<>),
            //    typeof(FluentValidationCommandDecorator<>));

            // query processing
            container.RegisterSingle<SimpleQueryProcessor>();
            container.Register<IProcessQueries>(container.GetInstance<SimpleQueryProcessor>);
            container.RegisterManyForOpenGeneric(typeof(IHandleQueries<,>), assemblies);

            // binary data storage
            container.RegisterBinaryDataStorage(configuration);

            // verify container
            container.Verify();

            return container;
        }

        [DebuggerStepThrough]
        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container).GetService(serviceType);
        }

    }
}
