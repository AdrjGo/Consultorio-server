using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class TreatmentSummaryConfig : IEntityTypeConfiguration<TreatmentSummary>
    {
        public void Configure(EntityTypeBuilder<TreatmentSummary> builder)
        {
            builder.ToTable("treatment_summary");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("treatment_summary_id").IsRequired();
            builder.Property(t => t.PatientId).HasColumnName("patient_id").IsRequired();
            builder.Property(t => t.SubmodID).HasColumnName("submod_id").IsRequired();
            builder.Property(t => t.SummaryDate).HasColumnName("summary_date").IsRequired();

            builder.HasOne(t => t.Patient)
                   .WithOne(p => p.TreatmentSummaryDetail)
                   .HasForeignKey<TreatmentSummary>(t => t.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Submodule)
                   .WithOne(s => s.TreatmentSummary)
                   .HasForeignKey<TreatmentSummary>(t => t.SubmodID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}