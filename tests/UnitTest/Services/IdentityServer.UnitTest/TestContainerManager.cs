using Autofac;
using Fix.Environment.FeatureManagement;
using Fix.Environment.Shell;
using Fix.Security;
using Fix.Security.OpenAuthentication.Jwt;
using IdentityServer.Infrastructor;
using IdentityServer.UnitTest.Facades;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace IdentityServer.UnitTest
{
    public class TestContainerManager
    {
        private IContainer _autofacContainer;
        public IContainer Container => _autofacContainer;

        IDataInitializer DataInitializer => new DataInitializer();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UseInMemory">if true database will be use in memory </param>
        public TestContainerManager()
        {
            BuildContainer();
        }

        private static readonly object locker = new object();
        /// <summary>
        /// Container building
        /// </summary>
        /// <param name="UseInMemory"></param>
        /// <param name="userId"></param>
        /// <param name="corporationId"></param>
        public void BuildContainer(long userId = 1, long corporationId = 1)
        {
            //container created for create workscope builder
            var shellContainer = TestScopeBuilder.Instance.Build(null);
            var builder = shellContainer.Resolve<IFeatureContextBuilder>();
            var featureContext = builder.Build();
            var workScopeBuilder = shellContainer.Resolve<IWorkScopeBuilder>();

            var shellContainerBuilder = TestScopeBuilder.Instance.Build();
            shellContainerBuilder.RegisterInstance(new JwtConfig());

            lock (locker)
            {
                shellContainerBuilder.Register(c =>
            {
                DbContextOptions<IdentityServerDbContext> options;
                options = new DbContextOptionsBuilder<IdentityServerDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .Options;
                var db = new IdentityServerDbContext(options);
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                DataInitializer.InitializeData(db);
                return db;
            }).InstancePerDependency();
            }
            workScopeBuilder.Build(shellContainerBuilder, featureContext);

            var mockAuthenticationService = new Mock<IAuthenticationProvider>();
            mockAuthenticationService.Setup(_ => _.GetUserId()).Returns(userId);
            mockAuthenticationService.Setup(_ => _.GetCompanyId()).Returns(corporationId);
            shellContainerBuilder.RegisterInstance(mockAuthenticationService.Object).As<IAuthenticationProvider>();
            _autofacContainer = shellContainerBuilder.Build();
        }

        public TService Resolve<TService>()
        {
            return _autofacContainer.Resolve<TService>();
        }

        // laziness + thread safety
        private readonly Lazy<TestContainerManager> instance = new Lazy<TestContainerManager>(() => new TestContainerManager());

        public TestContainerManager Instance => instance.Value;
    }
}
