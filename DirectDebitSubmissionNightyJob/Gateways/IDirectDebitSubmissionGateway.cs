using DirectDebitApi.V1.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways
{
    public interface IDirectDebitSubmissionGateway
    {
        public Task<List<DirectDebit>> GetAllDirectDebitsListAsync(DirectDebitSubmissionQuery submissionQuery);
        Task<DirectDebitSubmission> GetDirectDebitsSubmissionAsync(DirectDebitSubmissionQuery submissionQuery);
        Task<IEnumerable<DirectDebitSubmission>> GetAllDirectDebitsSubmissionListAsync();

        public Task<bool> UploadFileAsync(DirectDebitSubmission directDebitSubmission);
    }
}
