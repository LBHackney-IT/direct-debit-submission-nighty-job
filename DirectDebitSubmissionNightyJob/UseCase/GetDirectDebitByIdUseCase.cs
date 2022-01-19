using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.UseCase.Interfaces;
using DirectDebitSubmissionNightyJob.Gateways;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class GetDirectDebitByIdUseCase : IGetDirectDebitByIdUseCase
    {
        private readonly IDirectDebitGateway _gateway;

        public GetDirectDebitByIdUseCase(IDirectDebitGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<DirectDebitResponseObject> ExecuteAsync(Guid id)
        {
            var data = await _gateway.GetDirectDebitByIdAsync(id).ConfigureAwait(false);
            return data?.ToResponse();
        }
    }
}
