using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class PaymentTreatmentConfig : IEntityTypeConfiguration<PaymentTreatment>
    {
        public void Configure(EntityTypeBuilder<PaymentTreatment> builder)
        {
            builder.ToTable("payment_treatment");
            builder.HasKey(pt => pt.Id);
            builder.Property(pt => pt.Id).HasColumnName("payment_treatment_id").IsRequired();
            builder.Property(pt => pt.PatientId).HasColumnName("patient_id").IsRequired();
            builder.Property(pt => pt.Amount).HasColumnName("amount").IsRequired();
            builder.Property(pt => pt.Method).HasColumnName("method").HasConversion<string>().IsRequired();
            builder.Property(pt => pt.RecivedBy).HasColumnName("recived_by").IsRequired();
            builder.Property(pt => pt.Observations).HasColumnName("observations");

            builder.Property(pt => pt.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(pt => pt.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone").HasDefaultValueSql("now()").IsRequired();
            builder.Property(pt => pt.UpdatedBy).HasColumnName("updated_by");
            builder.Property(pt => pt.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp without time zone");
            builder.Property(pt => pt.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(pt => pt.Patient)
                   .WithMany(p => p.PaymentTreatments)
                   .HasForeignKey(pt => pt.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pt => pt.Contract)
                   .WithMany(c => c.PaymentTreatments)
                   .HasForeignKey(pt => pt.ContractId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}