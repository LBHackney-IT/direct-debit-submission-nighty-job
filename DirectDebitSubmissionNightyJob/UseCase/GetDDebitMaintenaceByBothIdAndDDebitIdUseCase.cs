using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.Gateways;
using DirectDebitApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class GetDDebitMaintenaceByBothIdAndDDebitIdUseCase : IGetDDebitMaintenaceByBothIdAndDDebitIdUseCase
    {
        private readonly IDirectDebitMaintenanceGateway _gateway;

        public GetDDebitMaintenaceByBothIdAndDDebitIdUseCase(IDirectDebitMaintenanceGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<DirectDebitMaintenanceResponseObject> ExecuteAsync(Guid id, Guid directdebitId)
        {
            var data = await _gateway.GetDirectDebitMaintenanceByIdAsync(id, directdebitId).ConfigureAwait(false);

            return data.ToResponse();
        }
    }
}
