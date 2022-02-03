using DirectDebitApi.V1.Domain;
using System.Linq;

namespace DirectDebitApi.V1.Factories
{
    public static class DirectDebitExportFactory
    {
        public static DirectDebitExport ToExport(this DirectDebit entity)
        {
            return entity == null ? null : new DirectDebitExport
            {
                Status = entity.Status,
                Due = entity.PreferredDate,
                Name = string.Join(",", entity.AccountHolders.Select(x => x.Name)),
                AccountNumber = entity.AccountNumber,
                PRN = entity.ServiceUserNumber,
                Amount = entity.AdditionalAmount,
                Type = entity.TargetType.ToString(),
                Acc = entity.Acc,
                Fund = entity.Fund,
                Trans = entity.Trans
            };
        }
    }
}
