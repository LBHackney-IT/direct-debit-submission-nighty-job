using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitSubmissionNightyJob.Boundary.Request;
using DirectDebitSubmissionNightyJob.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateways
{
    public interface IDirectDebitGateway
    {
        public Task<DirectDebit> GetDirectDebitByIdAsync(Guid id);
        public Task<PagedResult<DirectDebit>> GetAllDirectDebitsAsync(DirectDebitQuery query);
        public Task<List<DirectDebit>> GetAllDirectDebitsByQueryAsync(DirectDebitSubmissionRequest query);
        public Task<List<DirectDebit>> GetAllDirectDebitsByPrnsync(string prn);
        public Task AddAsync(DirectDebit directDebit);
        public Task UpdateAsync(Guid id, DirectDebit directDebit);
    }
}
