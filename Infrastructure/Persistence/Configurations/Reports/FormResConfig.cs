using System.Text.Json;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class FormResConfig : IEntityTypeConfiguration<FormRes>
    {
        public void Configure(EntityTypeBuilder<FormRes> builder)
        {
            builder.ToTable("form_response");
            builder.HasKey(fr => fr.Id);
            builder.Property(fr => fr.Id).HasColumnName("form_response_id").IsRequired();
            builder.Property(fr => fr.FormVersionId).HasColumnName("form_version_id").IsRequired();
            builder.Property(fr => fr.PatientId).HasColumnName("patient_id").IsRequired();
            builder.Property(fr => fr.JsonResponse)
                .HasColumnName("json_schema")
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<object>(v, (JsonSerializerOptions?)null)!)
                .IsRequired();

            builder.Property(fr => fr.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(fr => fr.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone").HasDefaultValueSql("now()").IsRequired();
            builder.Property(fr => fr.UpdatedBy).HasColumnName("updated_by");
            builder.Property(fr => fr.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp without time zone");
            builder.Property(fr => fr.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(fr => fr.FormVersion)
                   .WithMany(fv => fv.FormResponse)
                   .HasForeignKey(fr => fr.FormVersionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(fr => fr.Patient)
                   .WithMany(p => p.FormResponse)
                   .HasForeignKey(fr => fr.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}