using DirectDebitSubmissionNightyJob.Boundary.Response;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using DirectDebitSubmissionNightyJob.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateway
{
    public class TenureApiService : ITenureApiService
    {
        private readonly IRestClient _restClient;

        public TenureApiService(HttpClient httpClient, IRestClient restClient)
        {
            _restClient = restClient;
            restClient.Init(httpClient);
        }

        public async Task<TenureResponse> GetTenureInformation(Guid targetId)
        {
            return await _restClient.GetAsync<TenureResponse>($"tenures/{targetId}", "Failed to retrieve tenure information");
        }
    }
}
