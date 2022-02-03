using DirectDebitSubmissionNightyJob.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateways
{
    public interface IDirectDebitMaintenanceGateway
    {
        public Task<List<DirectDebitMaintenance>> GetDirectDebitMaintenanceByDDIdAsync(Guid directDebitId);
        public Task<DirectDebitMaintenance> GetDirectDebitMaintenanceByIdAsync(Guid id, Guid directdebitId);
        public Task AddAsync(DirectDebitMaintenance directDebit);
        public Task UpdateAsync(Guid id, Guid directdebitId, DirectDebitMaintenance directDebit);
    }
}
