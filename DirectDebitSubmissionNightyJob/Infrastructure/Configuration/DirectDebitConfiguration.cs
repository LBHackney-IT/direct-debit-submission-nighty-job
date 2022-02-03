using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using DirectDebitSubmissionNightyJob.Domain.Enums;
using DirectDebitSubmissionNightyJob.Infrastructure;

namespace DirectDebitSubmissionNightyJob.Infrastructure.Configuration
{
    public class DirectDebitConfiguration : IEntityTypeConfiguration<DirectDebitDbEntity>
    {
        public void Configure(EntityTypeBuilder<DirectDebitDbEntity> builder)
        {
            builder?.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .HasColumnName("Id")
               .HasColumnType("uuid")
               .IsRequired();

            builder.Property(x => x.PreferredDate)
                .IsRequired();

            builder.Property(x => x.AdditionalAmount)
                .IsRequired();

            builder.Property(x => x.PaymentReference)
                .HasColumnType("varchar(256)")
                .HasMaxLength(256);

            builder.Property(x => x.BankAccountNumber)
                .HasMaxLength(256)
                .HasColumnType("varchar(256)");

            builder.Property(t => t.TargetType)
                .HasConversion(v => v.ToString(), v => (TargetTypeEnum) Enum.Parse(typeof(TargetTypeEnum), v));

            builder.Property(x => x.AccountHolders)
              .HasColumnType("jsonb")
              .IsRequired();

            builder.Property(x => x.BankOrBuildingSociety)
           .HasColumnType("jsonb")
            .IsRequired();



            builder.HasMany(d => d.DirectDebitMaintenance)
               .WithOne(x => x.DirectDebit)
               .HasForeignKey(dt => dt.DirectDebitId);
        }
    }
}
