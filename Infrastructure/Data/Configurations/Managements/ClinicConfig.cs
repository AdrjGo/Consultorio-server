using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class ClinicConfig : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.ToTable("clinic");
            builder.HasKey(c => c.Id).HasName("clinic_id");
            builder.Property(c => c.Id).HasColumnName("clinic_id").IsRequired();
            builder.Property(c => c.ClinicName).HasColumnName("clinic_name").HasMaxLength(20).IsRequired();
            builder.Property(c => c.ClinicAddress).HasColumnName("clinic_address").HasMaxLength(50).IsRequired();
            builder.OwnsOne(c => c.ClinicPhone, clinicPhone =>
            {
                clinicPhone.Property(c => c.Value).HasColumnName("clinic_phone").HasMaxLength(10).IsRequired();
            });

            builder.OwnsOne(c => c.ClinicCellPhone, clinicCellPhone =>
            {
                clinicCellPhone.Property(c => c.Value).HasColumnName("clinic_cell_phone").HasMaxLength(10).IsRequired();
            });

            builder.OwnsOne(c => c.ClinicEmail, clinicEmail =>
            {
                clinicEmail.Property(c => c.Value).HasColumnName("clinic_email").HasMaxLength(20).IsRequired();
            });

            builder.Property(c => c.LogoRef).HasColumnName("logo_ref").IsRequired(false);
            builder.Property(c => c.LogoUrl).HasColumnName("logo_url").IsRequired(false);
            builder.Property(c => c.ManagerId).HasColumnName("manager_id").IsRequired();

            builder.HasOne(c => c.Manager)
                   .WithOne(m => m.Clinic)
                   .HasForeignKey<Clinic>(c => c.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            builder.Property(p => p.State).HasColumnName("state").HasConversion<string>().IsRequired();
        }
    }
}