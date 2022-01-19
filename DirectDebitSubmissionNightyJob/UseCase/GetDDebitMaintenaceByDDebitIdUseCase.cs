using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Domain;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.Gateways;
using DirectDebitApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class GetDDebitMaintenaceByDDebitIdUseCase : IGetDDebitMaintenaceByDDebitIdUseCase
    {
        private readonly IDirectDebitMaintenanceGateway _gateway;

        public GetDDebitMaintenaceByDDebitIdUseCase(IDirectDebitMaintenanceGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<DirectDebitMaintenanceResponseObjectList> ExecuteAsync(Guid directDebitId)
        {
            DirectDebitMaintenanceResponseObjectList debitResponseObjectList = new DirectDebitMaintenanceResponseObjectList();
            List<DirectDebitMaintenance> maintenance =
                await _gateway.GetDirectDebitMaintenanceByDDIdAsync(directDebitId).ConfigureAwait(false);

            if (maintenance != null)
            {

                debitResponseObjectList.ResponseObjects =
                maintenance.Select(p => p.ToResponse()).ToList();
                return debitResponseObjectList;
            }

            return null;
        }
    }
}
