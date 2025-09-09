using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class EvidenceFilesConfig : IEntityTypeConfiguration<EvidenceFile>
    {
        public void Configure(EntityTypeBuilder<EvidenceFile> builder)
        {
            builder.ToTable("evidence_file");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("evidence_file_id").IsRequired();
            builder.Property(e => e.MonitoringId).HasColumnName("monitoring_id").IsRequired();
            builder.Property(e => e.Format).HasConversion<string>().HasColumnName("file_type").IsRequired();

            builder.OwnsOne(e => e.ExternalReference, externalReference =>
            {
                externalReference.Property(e => e.Value).HasColumnName("external_reference").HasMaxLength(200).IsRequired();

            });

            builder.OwnsOne(e => e.Reference, reference =>
            {
                reference.Property(e => e.Value).HasColumnName("reference").HasMaxLength(200).IsRequired();
            });

            builder.Property(e => e.Description).HasColumnName("description").HasMaxLength(200);

            builder.Property(e => e.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            builder.Property(e => e.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(e => e.Monitoring)
                   .WithMany(m => m.EvidenceFiles)
                   .HasForeignKey(e => e.MonitoringId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}