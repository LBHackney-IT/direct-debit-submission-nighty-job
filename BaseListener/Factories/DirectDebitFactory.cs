using DirectDebitApi.V1.Boundary.Request;
using DirectDebitApi.V1.Domain;
using DirectDebitApi.V1.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DirectDebitApi.V1.Factories
{
    public static class DirectDebitFactory
    {
        public static DirectDebit ToDomain(this DirectDebitDbEntity dbEntity) => dbEntity == null ? null : new DirectDebit
        {
            Id = dbEntity.Id,
            Acc = dbEntity.Acc,
            AccountHolders = dbEntity.AccountHolders,
            AccountNumber = dbEntity.AccountNumber,
            BankAccountNumber = dbEntity.BankAccountNumber,
            BranchSortCode = dbEntity.BranchSortCode,
            BankOrBuildingSociety = dbEntity.BankOrBuildingSociety,
            FirstPaymentDate = dbEntity.FirstPaymentDate,
            Fund = dbEntity.Fund,
            PaymentReference = dbEntity.PaymentReference,
            Reference = dbEntity.Reference,
            ServiceUserNumber = dbEntity.ServiceUserNumber,
            Status = dbEntity.Status,
            TargetId = dbEntity.TargetId,
            TargetType = dbEntity.TargetType,
            AdditionalAmount = dbEntity.AdditionalAmount,
            PreferredDate = dbEntity.PreferredDate,
            Trans = dbEntity.Trans,
            DirectDebitMaintenance = dbEntity.DirectDebitMaintenance != null ? dbEntity.DirectDebitMaintenance.Select(x => x.ToDomain()) : new List<DirectDebitMaintenance>()
        };

        public static DirectDebitDbEntity ToDatabase(this DirectDebit entity) => entity == null ? null : new DirectDebitDbEntity
        {
            Id = entity.Id,
            Acc = entity.Acc,
            AccountHolders = entity.AccountHolders,
            AccountNumber = entity.AccountNumber,
            BankAccountNumber = entity.BankAccountNumber,
            BranchSortCode = entity.BranchSortCode,
            BankOrBuildingSociety = entity.BankOrBuildingSociety,
            FirstPaymentDate = entity.FirstPaymentDate,
            Fund = entity.Fund,
            PaymentReference = entity.PaymentReference,
            Reference = entity.Reference,
            ServiceUserNumber = entity.ServiceUserNumber,
            Status = entity.Status,
            TargetId = entity.TargetId,
            TargetType = entity.TargetType,
            AdditionalAmount = entity.AdditionalAmount,
            PreferredDate = entity.PreferredDate,
            Trans = entity.Trans,

        };


        public static DirectDebit ToDirectDebitDomain(this DirectDebitRequest request)
        {
            return request == null ? null : new DirectDebit
            {
                Acc = request.Acc,
                AccountHolders = request.AccountHolders,
                AccountNumber = request.AccountNumber,
                BankAccountNumber = request.BankAccountNumber,
                BranchSortCode = request.BranchSortCode,
                BankOrBuildingSociety = request.BankOrBuildingSociety,
                FirstPaymentDate = request?.FirstPaymentDate,
                Fund = request.Fund,
                PaymentReference = request.PaymentReference,
                Reference = request.Reference,
                ServiceUserNumber = request.ServiceUserNumber,
                Status = request.Status.ToString(),
                TargetId = request.TargetId,
                TargetType = request.TargetType,
                AdditionalAmount = request.AdditionalAmount,
                PreferredDate = request.PreferredDate,
                Trans = request.Trans
            };
        }


    }
}
