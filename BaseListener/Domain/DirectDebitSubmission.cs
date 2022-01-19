using DirectDebitApi.V1.Boundary.Response;
using System;
using System.Collections.Generic;

namespace DirectDebitApi.V1.Domain
{
    public class DirectDebitSubmission
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FileName { get; set; }
        public List<DirectDebit> DirectDebits { get; set; }
        public int DateOfCollection { get; set; }
        public string Status { get; set; }
        public ResultSummaryResponse PTXSubmissionResponse { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
