using DirectDebitSubmissionNightyJob.Boundary;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.UseCase.Interfaces
{
    public interface IMessageProcessing
    {
        Task ProcessMessageAsync(ILogger logger);
    }
}
