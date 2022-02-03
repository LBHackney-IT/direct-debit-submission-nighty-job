using DirectDebitApi.V1.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DirectDebitApi.V1.Infrastructure.Configuration
{
    public class DirectDebitMaintenanceConfiguration : IEntityTypeConfiguration<DirectDebitMaintenanceDbEntity>
    {
        public void Configure(EntityTypeBuilder<DirectDebitMaintenanceDbEntity> builder)
        {
            builder?.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
               .HasColumnType("uuid")
               .IsRequired();

            //builder.Property(x => x.DirectDebitId)
            //   .IsRequired();

            //builder.Property(x => x.PreviousAdditionalAmount)
            //   .IsRequired();

            //builder.Property(x => x.PreviousPreferredDate)
            //  .IsRequired();
            //builder.Property(x => x.PreviousStatus)
            // .IsRequired();
            //builder.Property(x => x.NewAdditionalAmount)
            //  .IsRequired();

            //builder.Property(x => x.NewPreferredDate)
            //  .IsRequired();
            //builder.Property(x => x.NewStatus)
            // .IsRequired();


            builder.Property(t => t.Status)
                .HasConversion(v => v.ToString(), v => (StatusEnum) Enum.Parse(typeof(StatusEnum), v));

            builder.HasOne(d => d.DirectDebit)
                .WithMany(t => t.DirectDebitMaintenance)
                .HasForeignKey(dt => dt.DirectDebitId);
        }
    }
}
