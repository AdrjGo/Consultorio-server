using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patient");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("patient_id").IsRequired();
            builder.Property(p => p.PersonId).HasColumnName("person_id").IsRequired();
            builder.Property(p => p.ResponsibleId).HasColumnName("responsible_id");
            builder.Property(p => p.Address).HasColumnName("address").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Zone).HasColumnName("zone").HasMaxLength(20).IsRequired();
            builder.Property(p => p.City).HasColumnName("city").HasMaxLength(20).IsRequired();

            builder.OwnsOne(p => p.HomePhone, homePhone =>
            {
                homePhone.Property(p => p.Value).HasColumnName("home_phone").HasMaxLength(10).IsRequired();
            });

            builder.Property(p => p.Occupation).HasColumnName("occupation").HasMaxLength(30).IsRequired();
            builder.Property(p => p.PlaceOccupation).HasColumnName("place_occupation").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Nit).HasColumnName("nit").HasMaxLength(12);
            builder.Property(p => p.Sender).HasColumnName("sender").HasMaxLength(0).IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            builder.Property(p => p.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(p => p.Person)
                   .WithOne(per => per.Patient)
                   .HasForeignKey<Patient>(p => p.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.PatientResponsible)
                   .WithOne(pr => pr.Patient)
                   .HasForeignKey<Patient>(p => p.ResponsibleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}