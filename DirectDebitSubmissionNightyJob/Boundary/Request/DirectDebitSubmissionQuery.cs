using System;

namespace DirectDebitApi.V1.Boundary.Request
{
    public class DirectDebitSubmissionQuery
    {
        public string Status { get; set; }
        public DateTime? DateToCollectAmount { get; set; }
    }
}
