using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateway.Interfaces
{
    public interface IDirectDebitGateway
    {
        public Task<DirectDebit> GetDirectDebitByIdAsync(Guid id);
        public Task<List<DirectDebit>> GetAllDirectDebitsByQueryAsync(DirectDebitSubmissionRequest query);

    }
}
