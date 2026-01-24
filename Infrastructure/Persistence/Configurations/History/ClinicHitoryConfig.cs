using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class ClinicHistoryConfig : IEntityTypeConfiguration<ClinicHistory>
    {
        public void Configure(EntityTypeBuilder<ClinicHistory> builder)
        {
            builder.ToTable("clinic_history");
            builder.HasKey(ch => ch.Id);
            builder.Property(ch => ch.Id).HasColumnName("clinic_history_id").IsRequired();
            builder.Property(ch => ch.PatientId).HasColumnName("patient_id").IsRequired();
            builder.Property(ch => ch.SubmodId).HasColumnName("submod_id").IsRequired();

            builder.Property(ch => ch.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(ch => ch.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone").HasDefaultValueSql("now()").IsRequired();
            builder.Property(ch => ch.UpdatedBy).HasColumnName("updated_by");
            builder.Property(ch => ch.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp without time zone");
            builder.Property(ch => ch.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(ch => ch.Patient)
                   .WithMany(p => p.ClinicHistories)
                   .HasForeignKey(ch => ch.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ch => ch.Submodule)
                   .WithOne(s => s.ClinicHistory)
                   .HasForeignKey<ClinicHistory>(ch => ch.SubmodId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}