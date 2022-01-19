using DirectDebitApi.V1.Boundary;
using DirectDebitApi.V1.Gateways.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways
{
    public class TransactionsApi : ITransactionsApi
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly ILogger<TransactionsApi> _logger;
        public TransactionsApi(IHttpClientFactory httpClientFactory, ILogger<TransactionsApi> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;

            _remoteServiceBaseUrl = Environment.GetEnvironmentVariable("TransactionApiUrl");
            if (string.IsNullOrEmpty(_remoteServiceBaseUrl) || !Uri.IsWellFormedUriString(_remoteServiceBaseUrl, UriKind.Absolute))
                throw new ArgumentException($"Configuration does not contain a setting value for the key {_remoteServiceBaseUrl}.");

            var token = Environment.GetEnvironmentVariable("TransactionApiToken");
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException($"Configuration does not contain a setting value for the key {token}.");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");


        }
        public async Task<TransactionRequest> CreateAccountWithTenure(TransactionRequest transactionRequest)
        {
            try
            {



                var response = await _httpClient.PostAsJsonAsync($"{_remoteServiceBaseUrl}/accounts", transactionRequest).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TransactionRequest>().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(result?.TargetId))
                        _logger.LogInformation($"Account successfully created for Tenure with TargetID: {result.TargetId}");
                    return result;

                }
                _logger.LogInformation($"Transaction Creation Failed");
                return null;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
