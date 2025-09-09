using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class SubmodulesConfig : IEntityTypeConfiguration<Submodule>
    {
        public void Configure(EntityTypeBuilder<Submodule> builder)
        {
            builder.ToTable("submodule");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).HasColumnName("submodule_id").IsRequired();
            builder.Property(s => s.Name).HasColumnName("submodule_name").HasMaxLength(20).IsRequired();

            builder.Property(s => s.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(s => s.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(s => s.UpdatedBy).HasColumnName("updated_by");
            builder.Property(s => s.UpdatedAt).HasColumnName("updated_at");
            builder.Property(s => s.State).HasColumnName("state").HasConversion<string>().IsRequired();
        }
    }
}