using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Boundary.Request;

namespace DirectDebitSubmissionNightyJob.Gateway.Interfaces
{
    public interface ITransactionApiService
    {
        Task CreateTransactionRecord(List<TransactionCreationRequest> transactionCreationRequests);
    }
}
