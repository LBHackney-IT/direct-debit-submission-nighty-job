using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.Gateways;
using DirectDebitApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class UpdateDirectDebitMaintenanceUseCase : IUpdateDirectDebitMaintenanceUseCase
    {
        private readonly IDirectDebitMaintenanceGateway _gateway;

        public UpdateDirectDebitMaintenanceUseCase(IDirectDebitMaintenanceGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<DirectDebitMaintenanceResponseObject> ExecuteAsync(Guid id, Guid directDebitId, DirectDebitMaintenanceRequest directDebitMaintenance)
        {
            if (directDebitMaintenance == null)
            {
                throw new ArgumentNullException(nameof(directDebitMaintenance));
            }
            var mainDomain = directDebitMaintenance.ToDirectDebitMaintenanceDomain();
            await _gateway.UpdateAsync(id, directDebitId, mainDomain).ConfigureAwait(false);
            return mainDomain.ToResponse();
        }
    }
}
