using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using DirectDebitSubmissionNightyJob.Services.Concrete;
using DirectDebitSubmissionNightyJob.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectDebitSubmissionNightyJob.Extension
{
    public static class ServiceExtensions
    {
        public static void ConfigureAccountApiClient(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpClient<IAccountApiService, AccountApiService>(client =>
                {
                    client.BaseAddress = new Uri(configuration["AccountAPI:BaseUrl"]);
                    client.DefaultRequestHeaders.Add("X-Api-Key", configuration["AccountAPI:ApiKey"]);
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
