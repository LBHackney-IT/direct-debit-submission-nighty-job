using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public interface IAddDirectDebitMaintenanceUseCase
    {
        public Task<DirectDebitMaintenanceResponseObject> ExecuteAsync(Guid directDebitId, DirectDebitMaintenanceRequest directDebit);
    }
}
