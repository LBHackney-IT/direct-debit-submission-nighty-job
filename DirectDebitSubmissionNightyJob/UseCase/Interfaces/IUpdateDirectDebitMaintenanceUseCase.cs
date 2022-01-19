using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public interface IUpdateDirectDebitMaintenanceUseCase
    {
        public Task<DirectDebitMaintenanceResponseObject> ExecuteAsync(Guid id, Guid directDebitId, DirectDebitMaintenanceRequest directDebit);
    }
}
