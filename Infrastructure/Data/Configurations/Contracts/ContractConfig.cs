using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class ContractConfig : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("contract");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("contract_id").IsRequired();
            builder.Property(c => c.SubmodID).HasColumnName("submod_id").IsRequired();
            builder.Property(c => c.PatientId).HasColumnName("patient_id").IsRequired();
            builder.Property(c => c.ContractDate).HasColumnName("contract_date").IsRequired();

            builder.Property(c => c.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(c => c.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(c => c.UpdatedBy).HasColumnName("updated_by");
            builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
            builder.Property(c => c.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(c => c.Submodule)
                   .WithOne(s => s.Contract)
                   .HasForeignKey<Contract>(c => c.SubmodID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Patient)
                   .WithOne(p => p.Contract)
                   .HasForeignKey<Contract>(c => c.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}