using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MonitoringConfig : IEntityTypeConfiguration<Monitoring>
    {
        public void Configure(EntityTypeBuilder<Monitoring> builder)
        {
            builder.ToTable("monitoring");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnName("monitoring_id").IsRequired();
            builder.Property(m => m.AppointmentId).HasColumnName("appointment_id").IsRequired();
            builder.Property(m => m.Nomenclature).HasColumnName("nomenclature").HasMaxLength(15).IsRequired();
            builder.Property(m => m.Treatment).HasColumnName("treatment").HasMaxLength(50).IsRequired();

            builder.Property(m => m.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(m => m.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(m => m.UpdatedBy).HasColumnName("updated_by");
            builder.Property(m => m.UpdatedAt).HasColumnName("updated_at");
            builder.Property(m => m.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(m => m.Appointment)
                   .WithOne(a => a.AppointmentMonitorings)
                   .HasForeignKey<Monitoring>(m => m.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}