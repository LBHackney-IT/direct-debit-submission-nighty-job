using DirectDebitSubmissionNightyJob.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Common;

public class MockWebApplicationFactory
{
    private readonly DbConnection _connection;
    public DirectDebitContext DatabaseContext { get; private set; }
    public MockWebApplicationFactory(DbConnection connection)
    {
        _connection = connection;
    }

    public IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
       .ConfigureAppConfiguration(b => b.AddEnvironmentVariables())
       .ConfigureServices((hostContext, services) =>
       {
           var dbBuilder = new DbContextOptionsBuilder();
           dbBuilder.UseNpgsql(_connection);
           var context = new DirectDebitContext(dbBuilder.Options);
           services.AddSingleton(context);

           var serviceProvider = services.BuildServiceProvider();
           DatabaseContext = serviceProvider.GetRequiredService<DirectDebitContext>();

           DatabaseContext.Database.EnsureCreated();

       });


}
