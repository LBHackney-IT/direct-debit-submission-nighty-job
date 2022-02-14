using DirectDebitSubmissionNightyJob.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateway.Interfaces
{
    public interface ITenureApiService
    {
        Task<TenureResponse> GetTenureInformation(Guid targetId);
    }
}