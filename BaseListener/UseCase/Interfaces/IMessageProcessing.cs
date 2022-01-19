<<<<<<< Updated upstream:BaseListener/UseCase/Interfaces/IMessageProcessing.cs
using BaseListener.Boundary;
=======
using Microsoft.Extensions.Logging;
>>>>>>> Stashed changes:DirectDebitSubmissionNightyJob/UseCase/Interfaces/IMessageProcessing.cs
using System.Threading.Tasks;

namespace BaseListener.UseCase.Interfaces
{
    public interface IMessageProcessing
    {
        Task ProcessMessageAsync(ILogger logger);
    }
}
