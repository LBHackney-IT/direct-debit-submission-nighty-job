using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DirectDebitSubmissionNightyJob.Domain.Enums;

namespace DirectDebitSubmissionNightyJob.Infrastructure
{
    [Table("direct_debit_maintenance")]
    public class DirectDebitMaintenanceDbEntity
    {
        public Guid Id { get; set; }
        public Guid DirectDebitId { get; set; }
        public decimal? PreviousAdditionalAmount { get; set; }
        public int? PreviousPreferredDate { get; set; }
        public string PreviousStatus { get; set; }
        public decimal? NewAdditionalAmount { get; set; }
        public int? NewPreferredDate { get; set; }
        public string NewStatus { get; set; }
        public int? PauseDuration { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public StatusEnum Status { get; set; }

        public virtual DirectDebitDbEntity DirectDebit { get; set; }
    }
}
