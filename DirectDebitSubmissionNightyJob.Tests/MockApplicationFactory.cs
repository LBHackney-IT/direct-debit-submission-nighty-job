using DirectDebitSubmissionNightyJob.Infrastructure;
using DirectDebitSubmissionNightyJob.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Collections.Generic;
using System.Data.Common;

namespace DirectDebitSubmissionNightyJob.Tests
{
    public class MockWebApplicationFactory
    {
        private readonly DbConnection _connection;
        public MockWebApplicationFactory(DbConnection connection)
        {
            _connection = connection;
            OutgoingRestClient = new Mock<IRestClient>();
        }

        public Mock<IRestClient> OutgoingRestClient { get; }

        public IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration(b => b.AddEnvironmentVariables())
          .ConfigureServices((hostContext, services) =>
          {

              var dbBuilder = new DbContextOptionsBuilder();
              dbBuilder.UseNpgsql(_connection);
              var context = new DirectDebitContext(dbBuilder.Options);
              ReplaceService(services, OutgoingRestClient.Object);
              services.AddSingleton(context);

              var serviceProvider = services.BuildServiceProvider();
              var dbContext = serviceProvider.GetRequiredService<DirectDebitContext>();

              dbContext.Database.EnsureCreated();
              var builder = new HostBuilder();
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

          });


        private static void ReplaceService<TService>(IServiceCollection services, TService replacement) where TService : class
        {
            services.RemoveAll<TService>();
            services.AddScoped(provider => replacement);
        }
    }
}
