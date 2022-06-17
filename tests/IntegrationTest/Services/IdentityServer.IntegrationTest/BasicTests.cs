using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using FluentAssertions;
using IdentityModel.Client;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace IdentityServer.IntegrationTest
{

    public class CustomHandler : DelegatingHandler
    {
        public CustomHandler(HttpMessageHandler handler) : base(handler)
        {

        }

    }
    public class BasicTests
        : IClassFixture<IdentityServerFactory<FakeStartup>>
    {
        private readonly IdentityServerFactory<FakeStartup> _factory;
        private readonly ITestOutputHelper testOutputHelper;

        public BasicTests(IdentityServerFactory<FakeStartup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            this.testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("api/Account/Show/5")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var x = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("integrationsettings.json", optional: true)
                .Build();
            var hostBuilder = Host.CreateDefaultBuilder(null)
                .UseEnvironment("Development")
                //.UseContentRoot("tests/IntegrationTest/Services/IdentityServer.IntegrationTest")
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseConfiguration(x)
                        .UseStartup<Startup>()
                        .UseUrls("http://localhost");
                });
            var identityUrl = "http://localhost/";//"http://35.205.187.151:100";//"http://localhost:65341"//"https://ahmetcagriakca.com"
            var webHostBuilder = WebHost.CreateDefaultBuilder(new string[] { })
                .ConfigureLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Warning);
                    builder.AddFilter("IdentityServer4", LogLevel.Debug);
                })
                .UseStartup<Startup>();
            var server = new TestServer(webHostBuilder);

            var handler = server.CreateHandler();
            Startup.Handler = handler;
            var client = new HttpClient(handler);
            client.BaseAddress = server.BaseAddress;

            var responseIdentity = await client.GetAsync(identityUrl + ".well-known/openid-configuration");
            var contentIdentity = await responseIdentity.Content.ReadAsStringAsync();

            var disco = await client.GetDiscoveryDocumentAsync();
            if (disco.IsError) Assert.Empty(disco.Error);

            // request token
            var tokenreq = new PasswordTokenRequest()
            {
                ClientId = "client",
                UserName = "admin",
                Password = "admin",
                Scope = "api1",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint,
                GrantType = "refresh_token"
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenreq);
            client.SetBearerToken(tokenResponse.AccessToken);
            // Act

            var responseIdentityService1 = await client.GetAsync(identityUrl + "index.html");
            var contentIdentityService1 = await responseIdentityService1.Content.ReadAsStringAsync();
            var responseIdentityService = await client.GetAsync("index.html");
            var contentIdentityService = await responseIdentityService.Content.ReadAsStringAsync();

            var response = await client.GetAsync(identityUrl + url);
            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("text/plain", response.Content.Headers.ContentType.ToString());
            //Assert.Equal("text/plain", response.Content.Headers.ContentType.ToString());
            // Assert
        }
        [Fact]
        public async Task IdentityServerLoginAndCallService()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("integrationsettings.json", optional: true)
                       .Build();
            using var host = await new HostBuilder()
                .UseEnvironment("Development")
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .Configure(app => { })
                        .UseConfiguration(configurationRoot)
                        .UseStartup<Startup>();
                })
                .StartAsync();

            var server = host.GetTestServer();

            var handler = server.CreateHandler();
            FakeStartup.Handler = handler;
            var client = new HttpClient(handler);
            client.BaseAddress = server.BaseAddress;

            // discover endpoints from metadata
            var disco = await client.GetDiscoveryDocumentAsync();
            disco.IsError.Should().Be(false, $"Error:{disco.Error}");
            
            var tokenreq = new PasswordTokenRequest()
            {
                ClientId = "client",
                UserName = "admin",
                Password = "admin",
                Scope = "api1",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint,
                GrantType = "refresh_token"
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenreq);
            tokenResponse.IsError.Should().Be(false, $"Error{tokenResponse.Error}");

            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("test");

            string responseString = null;
            if (response.Content != null)
            {
                responseString = await response.Content.ReadAsStringAsync();
                testOutputHelper.WriteLine(responseString);
            }

            response.StatusCode.Should().Be(200, $"StatusCode:{response.StatusCode}-ErrorDetail:{responseString}");
        }


        [Theory]
        [InlineData("api/Account/Show/5")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType1(string url)
        {

            var identityUrl = "http://localhost/";//"http://35.205.187.151:100";//"http://localhost:65341"//"https://ahmetcagriakca.com"
            var handler = _factory
                .WithWebHostBuilder(builder =>
                    builder.UseSolutionRelativeContentRoot(
                        "tests/IntegrationTest/Services/IdentityServer.IntegrationTest"))
                .Server
                .CreateHandler();
            var uri = new System.Uri(identityUrl);
            var han = new CustomHandler(handler);
            var s = new DelegatingHandler[]
            {
                han
            };
            FakeStartup.Handler = handler;
            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.UseSolutionRelativeContentRoot(
                        "tests/IntegrationTest/Services/IdentityServer.IntegrationTest"))
                .CreateDefaultClient(uri, s);//Client();


            var responseIdentity = await client.GetAsync(identityUrl + ".well-known/openid-configuration");
            var contentIdentity = await responseIdentity.Content.ReadAsStringAsync();
            var disco = await client.GetDiscoveryDocumentAsync();//("/");
            if (disco.IsError) Assert.Empty(disco.Error);
            var tokenreq = new PasswordTokenRequest()
            {
                ClientId = "client",
                UserName = "admin",
                Password = "admin",
                Scope = "api1",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint,
                GrantType = "refresh_token"
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenreq);
            tokenResponse.IsError.Should().Be(false, "Identity servera bağlanılamadı.");
            client.SetBearerToken(tokenResponse.AccessToken);
            // Act

            var responseIdentityService1 = await client.GetAsync(identityUrl + "index.html");
            var contentIdentityService1 = await responseIdentityService1.Content.ReadAsStringAsync();
            var responseIdentityService = await client.GetAsync("index.html");
            var contentIdentityService = await responseIdentityService.Content.ReadAsStringAsync();

            var response = await client.GetAsync(identityUrl + url);
            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            response.Content.Headers.ContentType.ToString().Should().Be("text/plain", "Content type must be text/plain");
            // Assert
        }
        [Theory]
        [InlineData("api/Account/Show/5")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType2(string url)
        {
            var identityUrl = "http://localhost/";//"http://35.205.187.151:100";//"http://localhost:65341"//"https://ahmetcagriakca.com"
            var fakeServer = _factory
                .WithWebHostBuilder(builder =>
                    builder.UseSolutionRelativeContentRoot(
                        "tests/IntegrationTest/Services/IdentityServer.IntegrationTest"));

            var client = fakeServer
                .CreateClient();//Client();

            var responseIdentity = await client.GetAsync(identityUrl + ".well-known/openid-configuration");
            var contentIdentity = await responseIdentity.Content.ReadAsStringAsync();

            var disco = await client.GetDiscoveryDocumentAsync();//("/");

            if (disco.IsError) Assert.Empty(disco.Error);

            // request token
            var tokenreq = new PasswordTokenRequest()
            {
                ClientId = "client",
                UserName = "admin",
                Password = "admin",
                Scope = "api1",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint,
                GrantType = "refresh_token"
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenreq);
            client.SetBearerToken(tokenResponse.AccessToken);
            // Act

            var responseIdentityService1 = await client.GetAsync(identityUrl + "index.html");
            var contentIdentityService1 = await responseIdentityService1.Content.ReadAsStringAsync();
            var responseIdentityService = await client.GetAsync("index.html");
            var contentIdentityService = await responseIdentityService.Content.ReadAsStringAsync();

            var response = await client.GetAsync(identityUrl + url);
            var content = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("text/plain", response.Content.Headers.ContentType.ToString());
            //Assert.Equal("text/plain", response.Content.Headers.ContentType.ToString());
            // Assert
        }


        [Theory]
        [InlineData("api/Account/Show/5")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType3(string url)
        {

            var identityUrl = "http://localhost/";//"http://35.205.187.151:100";//"http://localhost:65341"//"https://ahmetcagriakca.com"
            var x = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("integrationsettings.json", optional: true)
                .Build();
            var hostBuilder = Host.CreateDefaultBuilder(null)
                .UseEnvironment("Development")
                //.UseContentRoot("tests/IntegrationTest/Services/IdentityServer.IntegrationTest")
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseConfiguration(x)
                        .UseStartup<Startup>()
                        .UseUrls("http://localhost");
                });
            var services = hostBuilder.Build().Services;
            var server = new TestServer(services);

            var handler = server.CreateHandler();
            Startup.Handler = handler;
            var client = new HttpClient(handler);
            client.BaseAddress = server.BaseAddress;

            var responseIdentity = await client.GetAsync(identityUrl + ".well-known/openid-configuration");
            var contentIdentity = await responseIdentity.Content.ReadAsStringAsync();

            //var dc = new DiscoveryClient(client.BaseAddress.ToString(), handler);
            //var disco = await dc.GetAsync();
            var disco = await client.GetDiscoveryDocumentAsync();//("/");
            if (disco.IsError) Assert.Empty(disco.Error);

            // request token
            var tokenreq = new PasswordTokenRequest()
            {
                ClientId = "client",
                UserName = "admin",
                Password = "admin",
                Scope = "api1",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint,
                GrantType = "refresh_token"
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenreq);
            client.SetBearerToken(tokenResponse.AccessToken);
            // Act

            var responseIdentityService1 = await client.GetAsync(identityUrl + "index.html");
            var contentIdentityService1 = await responseIdentityService1.Content.ReadAsStringAsync();
            var responseIdentityService = await client.GetAsync("index.html");
            var contentIdentityService = await responseIdentityService.Content.ReadAsStringAsync();

            var response = await client.GetAsync(identityUrl + url);
            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("text/plain", response.Content.Headers.ContentType.ToString());
            //Assert.Equal("text/plain", response.Content.Headers.ContentType.ToString());
            // Assert
        }

        [Theory]
        [InlineData("api/Account/Show/5")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType4(string url)
        {

            var identityUrl = "http://localhost/";//"http://35.205.187.151:100";//"http://localhost:65341"//"https://ahmetcagriakca.com"
            var webHostBuilder = WebHost.CreateDefaultBuilder(new string[] { })
                .ConfigureLogging(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Warning);
                    builder.AddFilter("IdentityServer4", LogLevel.Debug);
                })
                .UseStartup<Startup>();
            ;
            var server = new TestServer(webHostBuilder);

            var handler = server.CreateHandler();
            Startup.Handler = handler;
            var client = new HttpClient(handler);
            client.BaseAddress = server.BaseAddress;

            var responseIdentity = await client.GetAsync(identityUrl + ".well-known/openid-configuration");
            var contentIdentity = await responseIdentity.Content.ReadAsStringAsync();

            var disco = await client.GetDiscoveryDocumentAsync();//("/");
            if (disco.IsError) Assert.Empty(disco.Error);

            // request token
            var tokenreq = new PasswordTokenRequest()
            {
                ClientId = "client",
                UserName = "admin",
                Password = "admin",
                Scope = "api1",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint,
                GrantType = "refresh_token"
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenreq);
            client.SetBearerToken(tokenResponse.AccessToken);
            // Act

            var responseIdentityService1 = await client.GetAsync(identityUrl + "index.html");
            var contentIdentityService1 = await responseIdentityService1.Content.ReadAsStringAsync();
            var responseIdentityService = await client.GetAsync("index.html");
            var contentIdentityService = await responseIdentityService.Content.ReadAsStringAsync();

            var response = await client.GetAsync(identityUrl + url);
            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode(); // Status Code 200-299

            Assert.Equal("text/plain", response.Content.Headers.ContentType.ToString());
        }

                [Fact]
        public async Task IdentityServerLoginAndCallServiceWithHost()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("integrationsettings.json", optional: true)
                       .Build();
            using var host = await new HostBuilder()
                .UseEnvironment("Development")
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .Configure(app => { })
                        .UseConfiguration(configurationRoot)
                        .UseStartup<Startup>();
                })
                .StartAsync();

            var server = host.GetTestServer();
            var client = host.GetTestClient();

            var handler = server.CreateHandler();
            Startup.Handler = handler;
            //var client = new HttpClient(handler);
            client.BaseAddress = server.BaseAddress;

            // discover endpoints from metadata
            var disco = await client.GetDiscoveryDocumentAsync();
            disco.IsError.Should().Be(false, $"Error:{disco.Error}");
            
            var tokenreq = new PasswordTokenRequest()
            {
                ClientId = "client",
                UserName = "admin",
                Password = "admin",
                Scope = "api1",
                ClientSecret = "secret",
                Address = disco.TokenEndpoint,
                GrantType = "refresh_token"
            };
            var tokenResponse = await client.RequestPasswordTokenAsync(tokenreq);
            tokenResponse.IsError.Should().Be(false, $"Error{tokenResponse.Error}");

            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("test");

            string responseString = null;
            if (response.Content != null)
            {
                responseString = await response.Content.ReadAsStringAsync();
                testOutputHelper.WriteLine(responseString);
            }

            response.StatusCode.Should().Be(200, $"StatusCode:{response.StatusCode}-ErrorDetail:{responseString}");
        }

    }
}
