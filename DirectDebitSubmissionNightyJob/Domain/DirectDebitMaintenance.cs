using DirectDebitSubmissionNightyJob.Domain.Enums;
using System;

namespace DirectDebitSubmissionNightyJob.Domain
{
    public class DirectDebitMaintenance
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid DirectDebitId { get; set; }
        public decimal? PreviousAdditionalAmount { get; set; }
        public int? PreviousPreferredDate { get; set; }
        public string PreviousStatus { get; set; }
        public decimal? NewAdditionalAmount { get; set; }
        public int? NewPreferredDate { get; set; }
        public string NewStatus { get; set; }
        public int? PauseDuration { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public StatusEnum Status { get; set; }
    }
}
