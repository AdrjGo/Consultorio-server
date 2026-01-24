using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("role_permission");
            builder.HasKey(rp => rp.Id);
            builder.Property(rp => rp.Id).HasColumnName("role_permission_id").IsRequired();
            builder.Property(rp => rp.RoleId).HasColumnName("role_id").IsRequired();
            builder.Property(rp => rp.PermissionId).HasColumnName("permission_id").IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone").HasDefaultValueSql("now()").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp without time zone");
            builder.Property(p => p.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(rp => rp.Role)
                   .WithMany(r => r.RolePermissions)
                   .HasForeignKey(rp => rp.RoleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rp => rp.Permission)
               .WithMany(p => p.RolePermissions)
               .HasForeignKey(rp => rp.PermissionId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}