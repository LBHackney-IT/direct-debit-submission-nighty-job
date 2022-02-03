using System.Collections.Generic;
using System.Linq;
using DirectDebitApi.V1.Boundary.Response;
using DirectDebitApi.V1.Domain;

namespace DirectDebitApi.V1.Factories
{
    public static class DirectDebitResponseFactory
    {

        public static DirectDebitResponseObject ToResponse(this DirectDebit domain)
        {
            return domain == null ? null : new DirectDebitResponseObject
            {
                Id = domain.Id,
                TargetId = domain.TargetId,
                TargetType = domain.TargetType,
                Acc = domain.Acc,
                AccountHolders = domain.AccountHolders,
                AccountNumber = domain.AccountNumber,
                BankAccountNumber = domain.BankAccountNumber,
                BranchSortCode = domain.BranchSortCode,
                BankOrBuildingSociety = domain.BankOrBuildingSociety,
                FirstPaymentDate = domain.FirstPaymentDate,
                Fund = domain.Fund,
                PaymentReference = domain.PaymentReference,
                Reference = domain.Reference,
                ServiceUserNumber = domain.ServiceUserNumber,
                Status = domain.Status,
                AdditionalAmount = domain.AdditionalAmount,
                PreferredDate = domain.PreferredDate,
                Trans = domain.Trans,
                CreatedDate = domain.CreatedDate,
                PauseDate = domain.PauseDate,
                PauseTillDate = domain.PauseTillDate,
                IsPause = domain.IsPause,
                CancellationDate = domain.CancellationDate,
                DirectDebitMaintenance = domain.DirectDebitMaintenance
            };
        }

        public static List<DirectDebitResponseObject> ToResponse(this IEnumerable<DirectDebit> domainList)
        {
            return domainList?.Select(domain => domain.ToResponse()).ToList();
        }

    }
}
