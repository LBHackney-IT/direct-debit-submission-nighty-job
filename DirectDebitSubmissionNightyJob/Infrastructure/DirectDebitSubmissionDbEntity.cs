using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DirectDebitSubmissionNightyJob.Boundary.Response;
using DirectDebitSubmissionNightyJob.Domain;

namespace DirectDebitSubmissionNightyJob.Infrastructure
{
    [Table("direct_debit_submission")]
    public class DirectDebitSubmissionDbEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public List<DirectDebit> DirectDebits { get; set; }
        public int DateOfCollection { get; set; }
        public string Status { get; set; }
        public ResultSummaryResponse PTXSubmissionResponse { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
