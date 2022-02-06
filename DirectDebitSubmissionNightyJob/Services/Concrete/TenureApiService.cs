using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Boundary.Response;
using DirectDebitSubmissionNightyJob.Boundary.Response.Account;
using DirectDebitSubmissionNightyJob.Helpers;
using DirectDebitSubmissionNightyJob.Services.Interfaces;

namespace DirectDebitSubmissionNightyJob.Services.Concrete
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
