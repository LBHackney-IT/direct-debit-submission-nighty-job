using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Domain;

namespace DirectDebitSubmissionNightyJob.UseCase.Interfaces
{
    public interface ICreateTransactionRecordUseCase
    {
        Task CreateTransactionRecordAsync(List<DirectDebit> directDebits);
    }
}
