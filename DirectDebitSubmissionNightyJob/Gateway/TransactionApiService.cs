using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using DirectDebitSubmissionNightyJob.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateway
{
    public class TransactionApiService : ITransactionApiService
    {
        private readonly IRestClient _restClient;

        public TransactionApiService(HttpClient httpClient, IRestClient restClient)
        {
            _restClient = restClient;
            restClient.Init(httpClient);
        }

        public async Task CreateTransactionRecord(List<TransactionCreationRequest> transactionCreationRequests)
        {
            await _restClient
                .PostAsync<object>($"transactions/process-batch", transactionCreationRequests, "Failed to create transaction records")
                .ConfigureAwait(false);
        }
    }
}
