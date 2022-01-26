using System;

namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public class DirectDebitSubmissionQuery
    {
        public string Status { get; set; }
        public DateTime? DateToCollectAmount { get; set; }
    }
}
