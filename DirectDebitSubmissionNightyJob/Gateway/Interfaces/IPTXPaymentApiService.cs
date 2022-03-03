using DirectDebitSubmissionNightyJob.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateway.Interfaces
{
    public interface IPTXPaymentApiService
    {
        Task<string> SubmitDirectDebitFile(byte[] bytes, string filename);
        Task<ResultSummaryResponse> GetResultSummaryByFileIdAsync(string id);
    }
}
