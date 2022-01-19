using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DirectDebitApi.V1.Factories
{
    public static class DirectDebitSubmissionResponseFactory
    {

        public static DirectDebitSubmissionResonseObject ToResponse(this DirectDebitSubmission domain)
        {
            return domain == null
                ? null
                : new DirectDebitSubmissionResonseObject
                {
                    Id = domain.Id,
                    FileName = domain.FileName,
                    DateOfCollection = domain.DateOfCollection,
                    DirectDebits = domain.DirectDebits != null ? domain.DirectDebits.ToResponse() : new List<DirectDebitResponseObject>(),
                    Status = domain.Status,
                    DateCreated = domain.DateCreated
                };
        }


        public static List<DirectDebitSubmissionResonseObject> ToResponse(this IEnumerable<DirectDebitSubmission> domainList)
        {
            return domainList?.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
