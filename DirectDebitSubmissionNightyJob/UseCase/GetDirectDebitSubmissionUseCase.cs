using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Factories;
using DirectDebitApi.V1.Gateways;
using DirectDebitApi.V1.UseCase.Interfaces;
using System.Threading.Tasks;

namespace DirectDebitApi.V1.UseCase
{
    public class GetDirectDebitSubmissionUseCase : IGetDirectDebitSubmissionUseCase
    {
        private readonly IDirectDebitSubmissionGateway _gateway;

        public GetDirectDebitSubmissionUseCase(IDirectDebitSubmissionGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<DirectDebitSubmissionResonseObject> ExecuteAsync(DirectDebitSubmissionQuery query)
        {

            var directDebitSubmission = await _gateway.GetDirectDebitsSubmissionAsync(query).ConfigureAwait(false);

            return directDebitSubmission.ToResponse();
        }
    }
}
