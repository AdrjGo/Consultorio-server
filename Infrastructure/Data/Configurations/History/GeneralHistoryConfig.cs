using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class GeneralHistoryConfig : IEntityTypeConfiguration<GeneralHistory>
    {
        public void Configure(EntityTypeBuilder<GeneralHistory> builder)
        {
            builder.ToTable("general_history");
            builder.HasKey(gh => gh.Id);
            builder.Property(gh => gh.Id).HasColumnName("general_history_id").IsRequired();
            builder.Property(gh => gh.PatientId).HasColumnName("patient_id").IsRequired();
            builder.Property(gh => gh.SubmodId).HasColumnName("submod_id").IsRequired();
            builder.Property(gh => gh.FilledBy).HasColumnName("filled_by").IsRequired();

            builder.Property(gh => gh.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(gh => gh.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(gh => gh.UpdatedBy).HasColumnName("updated_by");
            builder.Property(gh => gh.UpdatedAt).HasColumnName("updated_at");
            builder.Property(gh => gh.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(gh => gh.Patient)
                   .WithOne(p => p.GeneralHistory)
                   .HasForeignKey<GeneralHistory>(gh => gh.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(gh => gh.Submodule)
                   .WithOne(s => s.GeneralHistory)
                   .HasForeignKey<GeneralHistory>(gh => gh.SubmodId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}