using DirectDebitApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public interface IGetDDebitMaintenaceByDDebitIdUseCase
    {
        public Task<DirectDebitMaintenanceResponseObjectList> ExecuteAsync(Guid directDebitId);
    }
}
