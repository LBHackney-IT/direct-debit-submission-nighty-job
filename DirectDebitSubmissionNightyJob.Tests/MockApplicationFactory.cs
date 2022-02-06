using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using DirectDebitSubmissionNightyJob.Infrastructure;
using DirectDebitSubmissionNightyJob.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace DirectDebitSubmissionNightyJob.Tests
{
    public class MockWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly DbConnection _connection;
        public MockWebApplicationFactory(DbConnection connection)
        {
            _connection = connection;
            OutgoingRestClient = new Mock<IRestClient>();
        }

        public Mock<IRestClient> OutgoingRestClient { get; }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                ((ConfigurationBuilder) configBuilder).AddInMemoryCollection(
                     new Dictionary<string, string>
                     {
                         ["TenureApiUrl"] = "http://127.0.0.1",
                         ["TenureApiToken"] = "123",
                         ["TransactionApiUrl"] = "http://127.0.0.1",
                         ["TransactionApiToken"] = "123"
                     });
            });
            return base.CreateServer(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("IntegrationTesting");
            builder.ConfigureAppConfiguration(b => b.AddEnvironmentVariables())
                .UseStartup<SqsFunction>();
            builder.ConfigureServices(services =>
            {
                var dbBuilder = new DbContextOptionsBuilder();
                dbBuilder.UseNpgsql(_connection);
                var context = new DirectDebitContext(dbBuilder.Options);
                ReplaceService(services, OutgoingRestClient.Object);
                services.AddSingleton(context);

                var serviceProvider = services.BuildServiceProvider();
                var dbContext = serviceProvider.GetRequiredService<DirectDebitContext>();

                dbContext.Database.EnsureCreated();
            });
        }

        private static void ReplaceService<TService>(IServiceCollection services, TService replacement) where TService : class
        {
            services.RemoveAll<TService>();
            services.AddScoped(provider => replacement);
        }
    }
}
