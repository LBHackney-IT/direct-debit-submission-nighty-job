using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.Gateways;
using DirectDebitApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class AddDirectDebitMaintenanceUseCase : IAddDirectDebitMaintenanceUseCase
    {
        private readonly IDirectDebitMaintenanceGateway _gateway;

        public AddDirectDebitMaintenanceUseCase(IDirectDebitMaintenanceGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<DirectDebitMaintenanceResponseObject> ExecuteAsync(Guid directDebitId, DirectDebitMaintenanceRequest directDebit)
        {
            var directDomain = directDebit.ToDirectDebitMaintenanceDomain();
            directDomain.Id = Guid.NewGuid();
            directDomain.DirectDebitId = directDebitId;
            await _gateway.AddAsync(directDomain).ConfigureAwait(false);
            return directDomain.ToResponse();
        }
    }
}
