using System;
using System.Collections.Generic;

namespace DirectDebitApi.V1.Boundary.Request
{
    public class DirectDebitExportRequest
    {
        public string FileType { get; set; }
        public DateTime? Date { get; set; }
        public List<Guid> SelectedDirectDebitLists { get; set; }
    }
}
