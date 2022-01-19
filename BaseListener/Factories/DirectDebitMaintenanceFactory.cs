using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Domain;
using DirectDebitApi.V1.Infrastructure;

namespace DirectDebitApi.V1.Factories
{
    public static class DirectDebitMaintenanceFactory
    {
        public static DirectDebitMaintenance ToDomain(this DirectDebitMaintenanceDbEntity dbEntity)
        {
            if (dbEntity == null)
            {
                return null;
            }
            return new DirectDebitMaintenance
            {
                Id = dbEntity.Id,
                DirectDebitId = dbEntity.DirectDebitId,
                PreviousAdditionalAmount = dbEntity.PreviousAdditionalAmount,
                NewAdditionalAmount = dbEntity.NewAdditionalAmount,
                PreviousPreferredDate = dbEntity.PreviousPreferredDate,
                PreviousStatus = dbEntity.PreviousStatus,
                NewPreferredDate = dbEntity.NewPreferredDate,
                NewStatus = dbEntity.NewStatus,
                Date = dbEntity.Date,
                Reason = dbEntity.Reason,
                Status = dbEntity.Status
            };
        }

        public static DirectDebitMaintenanceDbEntity ToDatabase(this DirectDebitMaintenance entity)
        {
            return entity == null ? null : new DirectDebitMaintenanceDbEntity
            {
                Id = entity.Id,
                DirectDebitId = entity.DirectDebitId,
                PreviousAdditionalAmount = entity.PreviousAdditionalAmount.Value,
                NewAdditionalAmount = entity.NewAdditionalAmount.Value,
                PreviousPreferredDate = entity.PreviousPreferredDate.Value,
                PreviousStatus = entity.PreviousStatus,
                NewPreferredDate = entity.NewPreferredDate.Value,
                NewStatus = entity.NewStatus,
                Date = entity.Date,
                Reason = entity.Reason,
                Status = entity.Status
            };
        }


        public static DirectDebitMaintenance ToDirectDebitMaintenanceDomain(this DirectDebitMaintenanceRequest request)
        {
            return request == null ? null : new DirectDebitMaintenance
            {
                PreviousPreferredDate = request.PreviousPreferredDate.Value,
                PreviousAdditionalAmount = request.PreviousAdditionalAmount.Value,
                NewPreferredDate = request.NewPreferredDate.Value,
                PreviousStatus = request.PreviousStatus,
                NewAdditionalAmount = request.NewAdditionalAmount.Value,
                NewStatus = request.NewStatus,
                Date = request.Date,
                Reason = request.Reason,
                Status = request.Status,
                PauseDuration = request.PauseDuration.Value
            };
        }
    }
}
