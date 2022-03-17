using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DirectDebitSubmissionNightyJob.Domain;
using DirectDebitSubmissionNightyJob.Domain.Enums;

namespace DirectDebitSubmissionNightyJob.Infrastructure
{
    [Table("direct_debit")]
    public class DirectDebitDbEntity
    {
        public Guid Id { get; set; }
        public TargetTypeEnum TargetType { get; set; }
        public Guid TargetId { get; set; }
        public DateTime? FirstPaymentDate { get; set; }
        public string PaymentReference { get; set; }
        public string AccountNumber { get; set; }
        public string Fund { get; set; }
        public int Acc { get; set; }
        public int Trans { get; set; }
        public string BankAccountNumber { get; set; }
        public string BranchSortCode { get; set; }
        public string ServiceUserNumber { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public decimal AdditionalAmount { get; set; }
        public int PreferredDate { get; set; }
        public string Status { get; set; }
        public List<AccountHolder> AccountHolders { get; set; }
        public BankOrBuildingSociety BankOrBuildingSociety { get; set; }
        public SenderInformation SenderInformation { get; set; }
        public DateTime? CancellationDate { get; set; }
        public bool IsPaused { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? PauseTillDate { get; set; }
        public DateTime? PauseDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public virtual ICollection<DirectDebitMaintenanceDbEntity> DirectDebitMaintenance { get; set; }
    }
}
