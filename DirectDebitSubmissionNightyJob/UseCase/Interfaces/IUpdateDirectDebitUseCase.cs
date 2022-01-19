using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public interface IUpdateDirectDebitUseCase
    {
        public Task<DirectDebitResponseObject> ExecuteAsync(Guid id, DirectDebitRequest directDebit);
    }
}
