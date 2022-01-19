using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.Helpers;
using DirectDebitSubmissionNightyJob.Gateways;
using System.Linq;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase.Interfaces
{
    public class GetAllDirectDebitUseCase : IGetAllDirectDebitUseCase
    {
        private readonly IDirectDebitGateway _gateway;

        public GetAllDirectDebitUseCase(IDirectDebitGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<PagedResult<DirectDebitResponseObject>> ExecuteAsync(DirectDebitQuery query)
        {

            var directDebits =
                await _gateway.GetAllDirectDebitsAsync(query).ConfigureAwait(false);

            var responseObjects = new PagedResult<DirectDebitResponseObject>
            {
                Results = directDebits.Results.Select(p => p.ToResponse()).ToList(),
                TotalPages = directDebits.TotalPages,
                PageSize = directDebits.PageSize,
                TotalCount = directDebits.TotalCount,
                CurrentPage = directDebits.CurrentPage,
                Residents = directDebits.TotalCount,
                TotalAmount = directDebits.Results.Sum(p => p.AdditionalAmount)

            };
            return responseObjects;
        }
    }
}
