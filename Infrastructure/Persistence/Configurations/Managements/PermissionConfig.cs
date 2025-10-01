using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permission");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("permission_id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("permission_name").HasMaxLength(25).IsRequired();
            builder.Property(p => p.Description).HasColumnName("permission_description").HasMaxLength(100);

            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            builder.Property(p => p.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasMany(p => p.RolePermissions)
                   .WithOne(rp => rp.Permission)
                   .HasForeignKey(rp => rp.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}