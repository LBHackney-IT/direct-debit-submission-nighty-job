using System;
using System.Collections.Generic;
using System.Text;

namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public class TransactionCreationRequest
    {
        public TransactionType TransactionType { get; set; }
        public Guid TargetId { get; set; }
        public TargetType TargetType { get; set; }
        public int PeriodNo { get; set; }
        public string TransactionSource { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public string PaymentReference { get; set; }
        public string BankAccountNumber { get; set; }
        public string SortCode { get; set; }
        public double PaidAmount { get; set; }
        public double ChargedAmount { get; set; }
        public int BalanceAmount { get; set; }
        public double HousingBenefitAmount { get; set; }
        public string Address { get; set; }
        public Person Person { get; set; }
        public string Fund { get; set; }
        public int FinancialYear { get; set; }
        public int FinancialMonth { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
