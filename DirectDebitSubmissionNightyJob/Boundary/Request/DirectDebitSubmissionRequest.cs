using System;
using System.Collections.Generic;

namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public class DirectDebitSubmissionRequest
    {
        public string FileName { get; set; }
        public List<Guid> DirectDebits { get; set; }
        public int? DateOfCollection { get; set; }
        public string Status { get; set; }
    }
}
