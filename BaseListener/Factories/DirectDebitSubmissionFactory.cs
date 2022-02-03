using DirectDebitApi.V1.Domain;
using DirectDebitApi.V1.Infrastructure;

namespace DirectDebitApi.V1.Factories
{
    public static class DirectDebitSubmissionFactory
    {
        public static DirectDebitSubmissionDbEntity ToDatabase(this DirectDebitSubmission entity)
        {
            return entity == null ? null : new DirectDebitSubmissionDbEntity
            {
                Id = entity.Id,
                FileName = entity.FileName,
                DateOfCollection = entity.DateOfCollection,
                DirectDebits = entity.DirectDebits,
                Status = entity.Status,
                PTXSubmissionResponse = entity.PTXSubmissionResponse,
                DateCreated = entity.DateCreated
            };
        }

        public static DirectDebitSubmission ToDomain(this DirectDebitSubmissionDbEntity domain)
        {
            return domain == null ? null : new DirectDebitSubmission
            {
                Id = domain.Id,
                FileName = domain.FileName,
                DateOfCollection = domain.DateOfCollection,
                DirectDebits = domain.DirectDebits,
                Status = domain.Status,
                PTXSubmissionResponse = domain.PTXSubmissionResponse,
                DateCreated = domain.DateCreated
            };
        }
    }
}
