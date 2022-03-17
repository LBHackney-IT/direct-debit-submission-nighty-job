using DirectDebitSubmissionNightyJob.Domain.Enums;
using System;
using System.Collections.Generic;

namespace DirectDebitSubmissionNightyJob.Domain
{
    public class DirectDebit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public TargetTypeEnum TargetType { get; set; }
        public Guid TargetId { get; set; }
        public DateTime? FirstPaymentDate { get; set; }
        /// <summary>
        /// To be collected via Api or pass by client
        /// </summary>
        public string PaymentReference { get; set; }
        public string AccountNumber { get; set; }
        public string Fund { get; set; }
        public int Acc { get; set; }
        public int Trans { get; set; }
        public List<AccountHolder> AccountHolders { get; set; }
        public string BankAccountNumber { get; set; }
        public string BranchSortCode { get; set; }
        public string ServiceUserNumber { get; set; }
        public string Reference { get; set; }
        public BankOrBuildingSociety BankOrBuildingSociety { get; set; }
        public SenderInformation SenderInformation { get; set; }
        public decimal Amount { get; set; }
        public decimal AdditionalAmount { get; set; }
        public int PreferredDate { get; set; }
        public string Status { get; set; }
        public IEnumerable<DirectDebitMaintenance> DirectDebitMaintenance { get; set; }
        public DateTime? CancellationDate { get; set; }
        public bool IsPaused { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? PauseTillDate { get; set; }
        public DateTime? PauseDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}
