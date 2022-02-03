using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Domain;

namespace DirectDebitSubmissionNightyJob.UseCase.Interfaces
{
    public interface IUpdateRentAccountUseCase
    {
        Task ExecuteAsync(DirectDebit directDebit);
    }
}
