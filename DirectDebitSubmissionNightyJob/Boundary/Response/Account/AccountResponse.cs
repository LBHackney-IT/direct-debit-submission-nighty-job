using System;
using System.Collections.Generic;
using System.Text;
using DirectDebitSubmissionNightyJob.Domain.Account;

namespace DirectDebitSubmissionNightyJob.Boundary.Response.Account
{
    public class AccountResponse
    {
        public Guid Id { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal AccountBalance { get; set; } = 0;
        public decimal ConsolidatedBalance { get; set; } = 0;
        public IEnumerable<ConsolidatedCharge> ConsolidatedCharges { get; set; }
        public Guid ParentAccountId { get; set; }
        public string PaymentReference { get; set; }
        public string EndReasonCode { get; set; }
        public Guid TargetId { get; set; }
        public TargetType TargetType { get; set; }
        public AccountType AccountType { get; set; }
        public RentGroupType RentGroupType { get; set; }
        public string AgreementType { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public Tenure Tenure { get; set; }
    }
}
