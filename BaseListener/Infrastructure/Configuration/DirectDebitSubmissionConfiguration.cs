using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectDebitApi.V1.Infrastructure.Configuration
{
    public class DirectDebitSubmissionConfiguration : IEntityTypeConfiguration<DirectDebitSubmissionDbEntity>
    {
        public void Configure(EntityTypeBuilder<DirectDebitSubmissionDbEntity> builder)
        {
            builder?.HasKey(x => x.Id);
            builder.Property(x => x.DirectDebits)
             .HasColumnType("jsonb")
             .IsRequired();

            builder.Property(x => x.PTXSubmissionResponse)
            .HasColumnType("jsonb");
        }
    }

}
