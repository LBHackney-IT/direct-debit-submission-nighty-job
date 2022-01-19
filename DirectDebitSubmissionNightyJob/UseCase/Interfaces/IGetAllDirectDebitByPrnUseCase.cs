using DirectDebitApi.V1.Boundary.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public interface IGetAllDirectDebitByPrnUseCase
    {
        Task<List<DirectDebitResponseObject>> ExecuteAsync(string prn);
    }
}
