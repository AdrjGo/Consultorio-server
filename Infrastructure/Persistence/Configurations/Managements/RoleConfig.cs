using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("role");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnName("role_id").IsRequired();
            builder.Property(r => r.Name).HasColumnName("role_name").HasMaxLength(20).IsRequired();
            builder.Property(r => r.Description).HasColumnName("role_description").HasMaxLength(100).IsRequired();

            builder.Property(r => r.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(r => r.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone").HasDefaultValueSql("now()").IsRequired();
            builder.Property(r => r.UpdatedBy).HasColumnName("updated_by");
            builder.Property(r => r.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp without time zone");
            builder.Property(r => r.State).HasColumnName("state").HasConversion<string>().IsRequired();
        }
    }
}