using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateways
{
    public interface IDirectDebitExportGateway
    {
        public Task<List<DirectDebit>> GetAllDirectDebitsListAsync(DirectDebitExportRequest debitExportRequest);
    }
}
