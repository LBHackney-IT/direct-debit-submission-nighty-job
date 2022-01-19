using DirectDebitApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.Gateways
{
    public interface IDirectDebitMaintenanceGateway
    {
        public Task<List<DirectDebitMaintenance>> GetDirectDebitMaintenanceByDDIdAsync(Guid directDebitId);
        public Task<DirectDebitMaintenance> GetDirectDebitMaintenanceByIdAsync(Guid id, Guid directdebitId);
        public Task AddAsync(DirectDebitMaintenance directDebit);
        public Task UpdateAsync(Guid id, Guid directdebitId, DirectDebitMaintenance directDebit);
    }
}
