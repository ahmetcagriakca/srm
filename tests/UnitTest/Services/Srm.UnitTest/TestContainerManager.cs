using Autofac;
using Fix.Environment.FeatureManagement;
using Fix.Environment.Shell;
using Fix.Security;
using Fix.Security.OpenAuthentication.Jwt;
using Microsoft.EntityFrameworkCore;
using Moq;
using Srm.UnitTest.Facades;
using SRM.Data;
using System;

namespace Srm.UnitTest
{
    public class TestContainerManager
    {
        private IContainer _autofacContainer;
        public IContainer Container => _autofacContainer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UseInMemory">if true database will be use in memory </param>
        public TestContainerManager(bool UseInMemory = true)
        {
            BuildContainer(UseInMemory);

            #region container manuel create



            //var builder = new ContainerBuilder();
            //builder.Register(c =>
            //{
            //    DbContextOptions<SRMDbContext> options;
            //    options = new DbContextOptionsBuilder<SRMDbContext>()
            //        .UseInMemoryDatabase(Guid.NewGuid().ToString())
            //        .Options;
            //    return new SRMDbContext(options);
            //}).InstancePerLifetimeScope();
            //builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            //builder.RegisterType<ShellContextBuilder>().As<IDepedencyContextBuilder>();
            //builder.RegisterType<WorkScopeBuilder>().As<IWorkScopeBuilder>();
            //builder.RegisterType<TypeFinder>().As<ITypeFinder>();
            //builder.RegisterType<DefaultAssemblyLoader>().As<IAssemblyLoader>();
            //builder.RegisterType<AspectPolicyBuilder>().As<IAspectPolicyBuilder>();

            ////Features management
            //{
            //    builder.RegisterType<FeatureContextBuilder>().As<IFeatureContextBuilder>().SingleInstance();
            //    builder.RegisterType<FeatureDirectoryService>().As<IFeatureDirectoryService>().SingleInstance();
            //    builder.RegisterType<FeaturePathProvider>().As<IFeaturePathProvider>().SingleInstance();
            //    builder.RegisterType<FeatureProvider>().As<IFeatureProvider>().SingleInstance();
            //}

            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            //if (registration != null)
            //{
            //    registration.Invoke(builder);
            //}


            //// Register the CompanyDataRepository for property injection not constructor allowing circular references
            //builder.RegisterType<DbContextLocator>().As<IDbContextLocator>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //builder.RegisterType<TransactionManager>().As<ITransactionManager>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //builder.RegisterType<RepositoryEvent>().As<ITransanctionEventHandler>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //builder.RegisterType<RepositoryEvent>().As<ITransanctionEventHandler>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //#region  Mock Provider


            ////IWorkContext workContext=new WorkContext();
            //var valueProvider = new Mock<IValueProvider>();

            //long userId = 1;
            //valueProvider.Setup(_ => _.UserId()).Returns(userId);
            //valueProvider.Setup(_ => _.CreatedOn()).Returns(DateTime.Now);
            //valueProvider.Setup(_ => _.CreatedOnUtc()).Returns(DateTime.UtcNow);
            //valueProvider.Setup(_ => _.ModifiedOn()).Returns(DateTime.Now);
            //valueProvider.Setup(_ => _.ModifiedOnUtc()).Returns(DateTime.UtcNow);

            //builder.RegisterInstance(valueProvider.Object).As<IValueProvider>();

            //var authenticationService = new Mock<IAuthenticationService>();


            //builder.RegisterInstance(authenticationService.Object).As<IAuthenticationService>();
            //#endregion

            //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>))
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);


            //builder.RegisterType<CryptoService>().As<ICryptoService>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //builder.RegisterType<AccountPermissions>().As<IPermissionProvider>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //builder.RegisterType<SimpleCacheManager>().As<ICacheManager>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //builder.RegisterType<AccountService>().As<IAccountService>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //builder.RegisterType<SecurityDomain>().As<ISecurityDomain>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //builder.RegisterType<PageService>().As<IPageService>()
            //    .InstancePerLifetimeScope()
            //    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //// Other wireups....

            //var container = builder.Build();

            //_autofacContainer = container;

            #endregion

            ////initialize user
            //// TODO: every time user will create. this may affect to test performance. must be changed
        }

        /// <summary>
        /// Container building
        /// </summary>
        /// <param name="UseInMemory"></param>
        /// <param name="userId"></param>
        /// <param name="corporationId"></param>
        public void BuildContainer(bool UseInMemory, long userId = 1, long corporationId = 1)
        {
            //container created for create workscope builder
            var shellContainer = TestScopeBuilder.Instance.Build(null);
            var builder = shellContainer.Resolve<IFeatureContextBuilder>();
            var featureContext = builder.Build();
            var workScopeBuilder = shellContainer.Resolve<IWorkScopeBuilder>();

            var shellContainerBuilder = TestScopeBuilder.Instance.Build();
            shellContainerBuilder.RegisterInstance(new JwtConfig());

            shellContainerBuilder.Register(c =>
            {
                if (UseInMemory)
                {
                    DbContextOptions<SrmDbContext> options;
                    options = new DbContextOptionsBuilder<SrmDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .EnableSensitiveDataLogging()
                        .Options;
                    var db = new SrmDbContext(options);
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                    return db;
                }
                else
                {
                    DbContextOptions<SrmDbContext> options;
                    options = new DbContextOptionsBuilder<SrmDbContext>()
                        .UseNpgsql(
                            "User ID=srm;Password=123456;Host=localhost;Port=5432;Database=Srm.Services.Api.TestNew;Pooling=true;")
                        .Options;
                    var db = new SrmDbContext(options);
                    return db;
                }
            }).InstancePerDependency(); //InstancePerLifetimeScope();
            workScopeBuilder.Build(shellContainerBuilder, featureContext);

            var mockAuthenticationService = new Mock<IAuthenticationProvider>();
            mockAuthenticationService.Setup(_ => _.GetUserId()).Returns(userId);
            mockAuthenticationService.Setup(_ => _.GetCompanyId()).Returns(corporationId);
            shellContainerBuilder.RegisterInstance(mockAuthenticationService.Object).As<IAuthenticationProvider>();
            // shellContainer.BeginLifetimeScope(containerBuilder =>
            //{

            //    //containerBuilder.Populate(mvcBuilder.Services);
            //});
            _autofacContainer = shellContainerBuilder.Build();
        }

        public TService Resolve<TService>()
        {
            return _autofacContainer.Resolve<TService>();
        }

        // laziness + thread safety
        private Lazy<TestContainerManager> instance = new Lazy<TestContainerManager>(() => new TestContainerManager());

        public TestContainerManager Instance => instance.Value;
    }
}
