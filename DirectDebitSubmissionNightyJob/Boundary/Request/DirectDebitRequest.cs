using DirectDebitSubmissionNightyJob.Domain.Enums;
using DirectDebitSubmissionNightyJob.Domain;
using System;
using System.Collections.Generic;

namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public class DirectDebitRequest
    {
        public TargetTypeEnum TargetType { get; set; }
        public Guid TargetId { get; set; }
        public DateTime? FirstPaymentDate { get; set; }
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
        public decimal AdditionalAmount { get; set; }
        /// <summary>
        /// number from 1 to 31 are the acceptable values
        /// </summary>
        public int PreferredDate { get; set; }
        /// <summary>
        /// New, Active, Cancelled are the acceptable values
        /// </summary>
        public string Status { get; set; }
    }
}
