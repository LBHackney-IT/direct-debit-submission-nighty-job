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
        //public static void ConfigureAccountApiClient(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var url = Environment.GetEnvironmentVariable("AccountAPIBaseUrl") ?? configuration["AccountAPI:BaseUrl"];
        //    services
        //        .AddHttpClient<IAccountApiService, AccountApiService>(client =>
        //        {
        //            client.BaseAddress = new Uri(url);
        //            client.DefaultRequestHeaders.Add("X-Api-Key", Environment.GetEnvironmentVariable("AccountAPIApiKey"));
        //        })
        //        .ConfigureMessageHandlers();
        //}

        //public static void ConfigureHousingSearchApiClient(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var url = Environment.GetEnvironmentVariable("HousingSearchAPIBaseUrl") ?? configuration["HousingSearchAPI:BaseUrl"];
        //    services
        //        .AddHttpClient<IHousingSearchApiService, HousingSearchApiService>(client =>
        //        {
        //            client.BaseAddress = new Uri(url);
        //            client.DefaultRequestHeaders.Add("X-Api-Key", Environment.GetEnvironmentVariable("HousingSearchAPIApiKey"));
        //        })
        //        .ConfigureMessageHandlers();
        //}

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
