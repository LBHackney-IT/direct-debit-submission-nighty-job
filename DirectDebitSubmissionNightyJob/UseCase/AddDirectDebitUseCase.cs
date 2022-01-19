using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.UseCase.Interfaces;
using DirectDebitSubmissionNightyJob.Gateways;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class AddDirectDebitUseCase : IAddDirectDebitUseCase
    {
        private readonly IDirectDebitGateway _gateway;

        public AddDirectDebitUseCase(IDirectDebitGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<DirectDebitResponseObject> ExecuteAsync(DirectDebitRequest directDebit)
        {
            var directDebitDomain = directDebit.ToDirectDebitDomain();
            await _gateway.AddAsync(directDebitDomain).ConfigureAwait(false);
            return directDebitDomain.ToResponse();
        }
    }
}
