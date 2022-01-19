using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public interface IAddDirectDebitUseCase
    {
        public Task<DirectDebitResponseObject> ExecuteAsync(DirectDebitRequest directDebit);
    }
}
