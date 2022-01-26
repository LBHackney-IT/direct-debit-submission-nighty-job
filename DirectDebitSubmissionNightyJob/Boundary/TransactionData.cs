using System;

namespace DirectDebitSubmissionNightyJob.Boundary
{

    public class Person
    {
        public string Id { get; set; }
        public string FullName { get; set; }
    }

    public class TransactionRequest
    {
        public string TargetId { get; set; }
        public int PeriodNo { get; set; }
        public string TransactionSource { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public string PaymentReference { get; set; }
        public string BankAccountNumber { get; set; }
        public bool IsSuspense { get; set; }
        public double PaidAmount { get; set; }
        public double ChargedAmount { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public int BalanceAmount { get; set; }
        public double HousingBenefitAmount { get; set; }
        public string Address { get; set; }
        public Person Person { get; set; }
        public string Fund { get; set; }
    }


}
