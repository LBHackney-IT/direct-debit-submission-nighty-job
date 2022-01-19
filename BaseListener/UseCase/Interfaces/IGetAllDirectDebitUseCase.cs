using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public interface IGetAllDirectDebitUseCase
    {
        public Task<PagedResult<DirectDebitResponseObject>> ExecuteAsync(DirectDebitQuery query);
    }
}
