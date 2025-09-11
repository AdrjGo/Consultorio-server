using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class PaymentManagerConfig : IEntityTypeConfiguration<PaymentManager>
    {
        public void Configure(EntityTypeBuilder<PaymentManager> builder)
        {
            builder.ToTable("payment_manager");
            builder.HasKey(pm => pm.Id);
            builder.Property(pm => pm.Id).HasColumnName("payment_manager_id").IsRequired();
            builder.Property(pm => pm.PersonId).HasColumnName("person_id").IsRequired();
            builder.Property(pm => pm.ContractId).HasColumnName("contract_id").IsRequired();
            builder.Property(pm => pm.Parentage).HasColumnName("parentage").HasMaxLength(20).IsRequired();

            builder.Property(pm => pm.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(pm => pm.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(pm => pm.UpdatedBy).HasColumnName("updated_by");
            builder.Property(pm => pm.UpdatedAt).HasColumnName("updated_at");
            builder.Property(pm => pm.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(pm => pm.Person)
                   .WithOne(p => p.PaymentManager)
                   .HasForeignKey<PaymentManager>(pm => pm.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pm => pm.Contract)
                   .WithMany(c => c.PaymentManager)
                   .HasForeignKey(pm => pm.ContractId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}