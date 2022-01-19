using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Factories;
using DirectDebitSubmissionNightyJob.Gateways;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public class GetAllDirectDebitByPrnUseCase : IGetAllDirectDebitByPrnUseCase
    {
        private readonly IDirectDebitGateway _gateway;

        public GetAllDirectDebitByPrnUseCase(IDirectDebitGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<List<DirectDebitResponseObject>> ExecuteAsync(string prn)
        {

            var directDebits =
                await _gateway.GetAllDirectDebitsByPrnsync(prn).ConfigureAwait(false);

            var responseObjects = directDebits.Select(p => p.ToResponse()).ToList();
            return responseObjects;
        }
    }
}
