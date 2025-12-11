using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class FormConfig : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.ToTable("form");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).HasColumnName("form_id").IsRequired();
            builder.Property(f => f.Name).HasColumnName("form_name").HasMaxLength(30).IsRequired();
            builder.Property(f => f.Description).HasColumnName("form_description").HasMaxLength(100).IsRequired();

            builder.Property(f => f.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(f => f.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(f => f.UpdatedBy).HasColumnName("updated_by");
            builder.Property(f => f.UpdatedAt).HasColumnName("updated_at");
            builder.Property(f => f.State).HasColumnName("state").HasConversion<string>().IsRequired();
        }
    }
}