using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Boundary.Request.Account;
using DirectDebitSubmissionNightyJob.Boundary.Response.Account;

namespace DirectDebitSubmissionNightyJob.Services.Interfaces
{
    public interface IAccountApiService
    {
        Task<AccountResponse> GetAccountInformationByPrn(string paymentReference);
        Task UpdateRentAccountBalance(AccountUpdateRequest request);
    }
}
