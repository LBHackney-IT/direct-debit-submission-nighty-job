using DirectDebitSubmissionNightyJob.Boundary;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Gateway.Interfaces;
using DirectDebitSubmissionNightyJob.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.UseCase
{
    public class CreateTransactionRecordUseCase : ICreateTransactionRecordUseCase
    {
        //private readonly ITenureApiService _tenureApiService;
        private readonly ITransactionApiService _transactionApiService;

        public CreateTransactionRecordUseCase(ITransactionApiService transactionApiService)
        {
            //_tenureApiService = tenureApiService;
            _transactionApiService = transactionApiService;
        }

        public async Task CreateTransactionRecordAsync(List<DirectDebit> directDebits)
        {
            var transactionCreationRequests = new List<TransactionCreationRequest>();

            foreach (var directDebit in directDebits)
            {
                //var tenureInfo = await _tenureApiService.GetTenureInformation(directDebit.TargetId).ConfigureAwait(false);

                var transactionCreationRequest = new TransactionCreationRequest()
                {
                    TransactionType = TransactionType.DirectDebit,
                    TargetId = directDebit.TargetId,
                    TargetType = TargetType.Tenure,
                    PeriodNo = GetWeekNumber(),
                    TransactionSource = "Direct Debit",
                    TransactionDate = DateTime.Now,
                    TransactionAmount = 0,
                    PaymentReference = directDebit.PaymentReference,
                    BankAccountNumber = directDebit.BankAccountNumber,
                    SortCode = directDebit.BranchSortCode,
                    PaidAmount = directDebit.AdditionalAmount,
                    ChargedAmount = 0,
                    BalanceAmount = 0,
                    HousingBenefitAmount = 0,
                    Address = directDebit?.SenderInformation?.Address,
                    Sender = new Sender()
                    {
                        Id = directDebit?.SenderInformation?.Id,
                        FullName = directDebit?.SenderInformation?.FullName
                    },
                    Fund = directDebit.Fund,
                    FinancialMonth = DateTime.UtcNow.Month,
                    FinancialYear = DateTime.UtcNow.Year,
                };

                transactionCreationRequests.Add(transactionCreationRequest);
            }

            await _transactionApiService.CreateTransactionRecord(transactionCreationRequests);
        }

        private static int GetWeekNumber()
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }
    }
}
