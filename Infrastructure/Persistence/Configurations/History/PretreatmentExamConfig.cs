using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class PretreatmentExamConfig : IEntityTypeConfiguration<PretreatmentExam>
    {
        public void Configure(EntityTypeBuilder<PretreatmentExam> builder)
        {
            builder.ToTable("pretreatment_exam");
            builder.HasKey(pe => pe.Id);
            builder.Property(pe => pe.Id).HasColumnName("pretreatment_exam_id").IsRequired();
            // builder.Property(pe => pe.HistoryId).HasColumnName("history_id").IsRequired();
            builder.Property(pe => pe.PatientId).HasColumnName("patient_id").IsRequired();
            builder.Property(pe => pe.Observations).HasColumnName("observations").HasMaxLength(200).IsRequired();
            builder.Property(pe => pe.Interconsultation).HasColumnName("interconsultation").HasMaxLength(1000).IsRequired();
            builder.Property(pe => pe.Piece).HasColumnName("piece").HasMaxLength(18).IsRequired();
            builder.Property(pe => pe.Caries).HasColumnName("caries").HasMaxLength(20);
            builder.Property(pe => pe.Treatment).HasColumnName("treatment").HasMaxLength(50);
            builder.Property(pe => pe.Cost).HasColumnName("cost").IsRequired();

            builder.Property(pe => pe.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(pe => pe.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp without time zone").HasDefaultValueSql("now()").IsRequired();
            builder.Property(pe => pe.UpdatedBy).HasColumnName("updated_by");
            builder.Property(pe => pe.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp without time zone");
            builder.Property(pe => pe.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(pe => pe.Patient)
                   .WithMany(pt => pt.PretreatmentExams)
                   .HasForeignKey(pe => pe.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            // builder.HasOne(pe => pe.GeneralHistory)
            //        .WithOne(gh => gh.PretreatmentExam)
            //        .HasForeignKey<PretreatmentExam>(pe => pe.HistoryId)
            //        .OnDelete(DeleteBehavior.Restrict);

            // builder.HasMany(pe => pe.TreatmentProgress)
            //        .WithOne(tp => tp.PretreatmentExam)
            //        .HasForeignKey(tp => tp.ExamId)
            //        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}