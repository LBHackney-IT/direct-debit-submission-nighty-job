using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectDebitApi.V1.Infrastructure
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
