using DirectDebitSubmissionNightyJob.Domain;
using System;
using System.Threading.Tasks;

namespace DirectDebitSubmissionNightyJob.Gateway.Interfaces
{
    public interface IDbEntityGateway
    {
        Task<DomainEntity> GetEntityAsync(Guid id);
        Task SaveEntityAsync(DomainEntity entity);
    }
}
