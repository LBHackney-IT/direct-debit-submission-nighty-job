using System;
using System.Collections.Generic;

namespace DirectDebitSubmissionNightyJob.Boundary.Request
{
    public class DirectDebitExportRequest
    {
        public string FileType { get; set; }
        public DateTime? Date { get; set; }
        public List<Guid> SelectedDirectDebitLists { get; set; }
    }
}
