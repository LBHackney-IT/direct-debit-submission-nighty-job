using System.Collections.Generic;
using System.Linq;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Domain;

namespace DirectDebitApi.V1.Factories
{
    public static class DirectDebitMaintenanceResponseFactory
    {

        public static DirectDebitMaintenanceResponseObject ToResponse(this DirectDebitMaintenance domain)
        {
            return domain == null
                ? null
                : new DirectDebitMaintenanceResponseObject
                {
                    Id = domain.Id,
                    DirectDebitId = domain.DirectDebitId,
                    PreviousPreferredDate = domain.PreviousPreferredDate,
                    PreviousAdditionalAmount = domain.PreviousAdditionalAmount,
                    PreviousStatus = domain.PreviousStatus,
                    NewPreferredDate = domain.NewPreferredDate,
                    NewAdditionalAmount = domain.NewAdditionalAmount,
                    NewStatus = domain.NewStatus,
                    Date = domain.Date,
                    PauseDuration = domain.PauseDuration,
                    Reason = domain.Reason,
                    Status = domain.Status
                };
        }

        public static List<DirectDebitMaintenanceResponseObject> ToResponse(this IEnumerable<DirectDebitMaintenance> domainList)
        {
            return domainList?.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
