using System;
using System.ComponentModel.DataAnnotations;

namespace DirectDebitSubmissionNightyJob.Domain.Account
{
    public class PrimaryTenants
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}
