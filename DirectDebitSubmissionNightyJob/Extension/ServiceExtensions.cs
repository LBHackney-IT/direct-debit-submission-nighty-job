using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using DirectDebitSubmissionNightyJob.Gateway;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectDebitSubmissionNightyJob.Extension
{
    public static class ServiceExtensions
    {
        public static void ConfigureTenureApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            var url = Environment.GetEnvironmentVariable("TenureApiBaseUrl") ?? configuration["TenureApi:BaseUrl"];
            var token = Environment.GetEnvironmentVariable("TenureApiToken") ?? "token";

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
            var url = Environment.GetEnvironmentVariable("TransactionApiBaseUrl") ?? configuration["TransactionApi:BaseUrl"];
            var token = Environment.GetEnvironmentVariable("TransactionApiToken") ?? "token";

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
