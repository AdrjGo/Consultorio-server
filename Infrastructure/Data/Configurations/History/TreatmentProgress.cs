using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class TreatmentProgressConfig : IEntityTypeConfiguration<TreatmentProgress>
    {
        public void Configure(EntityTypeBuilder<TreatmentProgress> builder)
        {
            builder.ToTable("treatment_progress");
            builder.HasKey(tp => tp.Id);
            builder.Property(tp => tp.Id).HasColumnName("treatment_progress_id").IsRequired();
            builder.Property(tp => tp.ExamId).HasColumnName("exam_id").IsRequired();
            builder.Property(tp => tp.Payment).HasColumnName("payment").IsRequired();
            builder.Property(tp => tp.Debt).HasColumnName("debt").IsRequired();

            builder.Property(tp => tp.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(tp => tp.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(tp => tp.UpdatedBy).HasColumnName("updated_by");
            builder.Property(tp => tp.UpdatedAt).HasColumnName("updated_at");
            builder.Property(tp => tp.State).HasColumnName("state").HasConversion<string>().IsRequired();
        }
    }
}