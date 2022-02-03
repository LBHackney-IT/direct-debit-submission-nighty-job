using System.Collections.Generic;

namespace DirectDebitSubmissionNightyJob.Domain.Account
{
    public class Tenure
    {
        public string TenureId { get; set; }
        public TenureType TenureType { get; set; }
        public string FullAddress { get; set; }
        public List<PrimaryTenants> PrimaryTenants { get; set; }
    }
}
