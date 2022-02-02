using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DirectDebitSubmissionNightyJob.Boundary.Response.Account;

namespace DirectDebitSubmissionNightyJob.Services.Interfaces
{
    public interface IHousingSearchApiService
    {
        Task<AccountResponse> GetRentAccountByPrn(string paymentReference);
    }
}
