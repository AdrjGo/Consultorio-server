using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class PatientResponsibleConfig : IEntityTypeConfiguration<PatientResponsible>
    {
        public void Configure(EntityTypeBuilder<PatientResponsible> builder)
        {
            builder.ToTable("patient_responsible");
            builder.HasKey(pr => pr.Id);
            builder.Property(pr => pr.Id).HasColumnName("patient_responsible_id").IsRequired();
            builder.Property(pr => pr.PersonId).HasColumnName("person_id").IsRequired();
            builder.Property(pr => pr.Parentage).HasColumnName("parentage").HasConversion<string>().IsRequired();

            builder.Property(pr => pr.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(pr => pr.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(pr => pr.UpdatedBy).HasColumnName("updated_by");
            builder.Property(pr => pr.UpdatedAt).HasColumnName("updated_at");
            builder.Property(pr => pr.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(pr => pr.Person)
                   .WithOne(p => p.PatientResponsible)
                   .HasForeignKey<PatientResponsible>(pr => pr.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}