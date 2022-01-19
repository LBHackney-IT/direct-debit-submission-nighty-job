using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways
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
