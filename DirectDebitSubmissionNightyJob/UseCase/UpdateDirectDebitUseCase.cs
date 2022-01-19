using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.UseCase.Interfaces;
using DirectDebitSubmissionNightyJob.Gateways;
using System;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class UpdateDirectDebitUseCase : IUpdateDirectDebitUseCase
    {
        private readonly IDirectDebitGateway _gateway;

        public UpdateDirectDebitUseCase(IDirectDebitGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<DirectDebitResponseObject> ExecuteAsync(Guid id, DirectDebitRequest directDebit)
        {
            if (directDebit == null)
            {
                throw new ArgumentNullException(nameof(directDebit));
            }
            var directDomain = directDebit.ToDirectDebitDomain();
            await _gateway.UpdateAsync(id, directDomain).ConfigureAwait(false);
            return directDomain.ToResponse();
        }
    }
}
