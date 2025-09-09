using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class UserRolConfig : IEntityTypeConfiguration<UserRol>
    {
        public void Configure(EntityTypeBuilder<UserRol> builder)
        {
            builder.ToTable("user_rol");
            builder.HasKey(ur => ur.Id);
            builder.Property(ur => ur.Id).HasColumnName("user_rol_id").IsRequired();
            builder.Property(ur => ur.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(ur => ur.RoleId).HasColumnName("role_id").IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            builder.Property(p => p.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserRols)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Role)
                   .WithMany(r => r.UserRols)
                   .HasForeignKey(ur => ur.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}