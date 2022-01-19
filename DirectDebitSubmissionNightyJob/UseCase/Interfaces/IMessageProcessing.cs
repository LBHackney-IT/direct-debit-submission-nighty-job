using DirectDebitSubmissionNightyJob.Boundary;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.UseCase.Interfaces
{
    public interface IMessageProcessing
    {
        Task ProcessMessageAsync(EntityEventSns message);
    }
}
