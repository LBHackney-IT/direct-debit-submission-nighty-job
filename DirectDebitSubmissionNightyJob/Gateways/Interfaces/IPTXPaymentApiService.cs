using DirectDebitSubmissionNightyJob.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways.Interfaces
{
    public interface IPTXPaymentApiService
    {

        Task<Tuple<bool, ResultSummaryResponse>> SubmitDirectDebitFile(byte[] bytes, string filename);
    }
}
