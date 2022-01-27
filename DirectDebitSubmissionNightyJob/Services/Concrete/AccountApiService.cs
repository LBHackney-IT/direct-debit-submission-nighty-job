using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Boundary.Request.Account;
using DirectDebitSubmissionNightyJob.Boundary.Response.Account;
using DirectDebitSubmissionNightyJob.Services.Interfaces;

namespace DirectDebitSubmissionNightyJob.Services.Concrete
{
    public class AccountApiService : IAccountApiService
    {
        private readonly IRestClient _restClient;

        public AccountApiService(HttpClient httpClient, IRestClient restClient)
        {
            _restClient = restClient;
            restClient.Init(httpClient);
        }

        public async Task<AccountResponse> GetAccountInformationByPrn(string paymentReference)
        {
            return await _restClient.GetAsync<AccountResponse>($"accounts/prn/{paymentReference}", "Failed to retrieve account");
        }

        public async Task UpdateRentAccountBalance(AccountUpdateRequest request) =>
            await _restClient
                .PatchAsync<object>($"accounts/{request.Id}", request, "Failed to update rent account")
                .ConfigureAwait(false);
    }
}
