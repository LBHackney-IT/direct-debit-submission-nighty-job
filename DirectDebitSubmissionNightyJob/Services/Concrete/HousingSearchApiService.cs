using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Boundary.Response.Account;
using DirectDebitSubmissionNightyJob.Helpers;
using DirectDebitSubmissionNightyJob.Services.Interfaces;

namespace DirectDebitSubmissionNightyJob.Services.Concrete
{
    public class HousingSearchApiService : IHousingSearchApiService
    {
        private readonly IRestClient _restClient;

        public HousingSearchApiService(HttpClient httpClient, IRestClient restClient)
        {
            _restClient = restClient;
            restClient.Init(httpClient);
        }

        public async Task<AccountResponse> GetRentAccountByPrn(string paymentReference)
        {
            var url = new UrlFormatter()
                .SetBaseUrl("/api/v1/search/accounts")
                .AddParameter("searchText", paymentReference)
                .ToString();

            return await _restClient
                .GetAsync<AccountResponse>(url, "Could not retrieve rent account information");
        }
    }
}
