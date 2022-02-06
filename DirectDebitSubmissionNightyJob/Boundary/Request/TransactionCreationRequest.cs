using System;

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
        public decimal TransactionAmount { get; set; }
        public string PaymentReference { get; set; }
        public string BankAccountNumber { get; set; }
        public string SortCode { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal ChargedAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal HousingBenefitAmount { get; set; }
        public string Address { get; set; }
        public Sender Sender { get; set; }
        public string Fund { get; set; }
        public int FinancialYear { get; set; }
        public int FinancialMonth { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
