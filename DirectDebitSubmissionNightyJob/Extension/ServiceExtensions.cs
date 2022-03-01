using DirectDebitSubmissionNightyJob.Gateway;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Security.Authentication;

namespace DirectDebitSubmissionNightyJob.Extension
{
    public static class ServiceExtensions
    {
        public static void ConfigureTenureApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            var url = Environment.GetEnvironmentVariable("TenureApiUrl") ?? throw new ArgumentException($"Configuration does not contain a setting value for the key TenureApiUrl.");
            var token = Environment.GetEnvironmentVariable("TenureApiToken") ?? throw new ArgumentException($"Configuration does not contain a setting value for the key TenureApiToken.");

            services
                .AddHttpClient<ITenureApiService, TenureApiService>(client =>
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                })
                .ConfigureMessageHandlers();
        }

        public static void ConfigureTransactionApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            var url = Environment.GetEnvironmentVariable("TransactionApiUrl") ?? throw new ArgumentException($"Configuration does not contain a setting value for the key TransactionApiUrl.");
            var token = Environment.GetEnvironmentVariable("TransactionApiToken") ?? throw new ArgumentException($"Configuration does not contain a setting value for the key TransactionApiToken."); ;

            services
                .AddHttpClient<ITransactionApiService, TransactionApiService>(client =>
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                })
                .ConfigureMessageHandlers();
        }

        private static IHttpClientBuilder ConfigureMessageHandlers(this IHttpClientBuilder builder)
        {
            return builder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                SslProtocols = SslProtocols.Tls12,
                AllowAutoRedirect = false,
                UseDefaultCredentials = true
            });
        }
    }
}
