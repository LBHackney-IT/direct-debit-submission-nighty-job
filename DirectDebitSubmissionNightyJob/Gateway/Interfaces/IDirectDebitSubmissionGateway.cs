using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateway.Interfaces
{
    public interface IDirectDebitSubmissionGateway
    {
        public Task<List<DirectDebit>> GetAllDirectDebitsListAsync(DirectDebitSubmissionQuery submissionQuery);
        Task<DirectDebitSubmission> GetDirectDebitsSubmissionAsync(DirectDebitSubmissionQuery submissionQuery);
        Task<IEnumerable<DirectDebitSubmission>> GetAllDirectDebitsSubmissionListAsync();

        public Task<bool> UploadFileAsync(DirectDebitSubmission directDebitSubmission);
    }
}
