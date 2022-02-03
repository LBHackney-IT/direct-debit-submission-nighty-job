using DirectDebitApi.V1.Boundary.Request;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public interface IAddDirectDebitSubmissionUseCase
    {
        public Task<bool> ExecuteAsync(DirectDebitSubmissionRequest submissionRequest);
    }
}
