using System.Text.Json;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class FormVersionConfig : IEntityTypeConfiguration<FormVersion>
    {
        public void Configure(EntityTypeBuilder<FormVersion> builder)
        {
            builder.ToTable("form_version");
            builder.HasKey(fv => fv.Id);
            builder.Property(fv => fv.Id).HasColumnName("form_version_id").IsRequired();
            builder.Property(fv => fv.FormId).HasColumnName("form_id").IsRequired();
            builder.Property(fv => fv.NumberVersion).HasColumnName("number_version").IsRequired();
            builder.Property(fv => fv.JsonSchema)
                .HasColumnName("json_schema")
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<object>(v, (JsonSerializerOptions?)null)!)
                .IsRequired();

            builder.Property(fv => fv.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(fv => fv.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(fv => fv.UpdatedBy).HasColumnName("updated_by");
            builder.Property(fv => fv.UpdatedAt).HasColumnName("updated_at");
            builder.Property(fv => fv.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(fv => fv.Form)
                   .WithMany(f => f.FormVersions)
                   .HasForeignKey(fv => fv.FormId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fv => fv.Submodule)
                   .WithMany(s => s.FormVersions)
                   .HasForeignKey(fv => fv.SubmodID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}