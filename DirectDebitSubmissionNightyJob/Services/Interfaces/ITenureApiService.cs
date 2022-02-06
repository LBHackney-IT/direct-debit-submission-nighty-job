using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Boundary.Response;

namespace DirectDebitSubmissionNightyJob.Services.Interfaces
{
    public interface ITenureApiService
    {
        Task<TenureResponse> GetTenureInformation(Guid targetId);
    }
}
