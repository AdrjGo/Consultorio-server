using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("appointment");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).HasColumnName("appointment_id").IsRequired();
            builder.Property(a => a.PatientId).HasColumnName("patient_id").IsRequired();
            builder.Property(a => a.ProfessionalId).HasColumnName("professional_id").IsRequired();
            builder.Property(a => a.StartDate).HasColumnName("start_date").HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(a => a.EndDate).HasColumnName("end_date").HasColumnType("timestamp without time zone").IsRequired();
            builder.Property(a => a.Type).HasConversion<string>().HasColumnName("appointment_type").IsRequired();
            builder.Property(a => a.Status).HasConversion<string>().HasColumnName("status").IsRequired();
            builder.Property(a => a.LifeStatus).HasConversion<string>().HasColumnName("life_status").IsRequired();
            builder.Property(a => a.Reason).HasColumnName("reason").HasMaxLength(100).IsRequired();
            builder.Property(a => a.Observations).HasColumnName("observations").HasMaxLength(1000).IsRequired();

            builder.Property(a => a.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(a => a.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(a => a.UpdatedBy).HasColumnName("updated_by");
            builder.Property(a => a.UpdatedAt).HasColumnName("updated_at");
            builder.Property(a => a.StartAt).HasColumnName("start_at");
            builder.Property(a => a.StartBy).HasColumnName("start_by");
            builder.Property(a => a.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(a => a.Professional)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.ProfessionalId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Patient)
               .WithMany(p => p.Appointments)
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}