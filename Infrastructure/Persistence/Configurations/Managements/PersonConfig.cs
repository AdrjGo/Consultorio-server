using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person");
            builder.HasKey(p => p.Id).HasName("person_id");
            builder.Property(p => p.Id).HasColumnName("person_id").IsRequired();
            builder.Property(p => p.Name).HasColumnName("person_name").HasMaxLength(15).IsRequired();
            builder.Property(p => p.LastName).HasColumnName("person_last_name").HasMaxLength(15).IsRequired();
            builder.Property(p => p.BirthDate).HasColumnName("birth_date").IsRequired();
            builder.Property(p=>p.Sex).HasConversion<string>().HasColumnName("sex").IsRequired();
            builder.Property(p => p.Ci).HasColumnName("ci").HasMaxLength(9).IsRequired();

            builder.OwnsOne(p => p.Email, email =>
            {
                email.Property(p => p.Value).HasColumnName("email").HasMaxLength(25).IsRequired();
            });

            builder.OwnsOne(p => p.Phone, phone =>
            {
                phone.Property(p => p.Value).HasColumnName("phone_number").HasMaxLength(10).IsRequired();
            });

            builder.Property(p => p.Profession).HasMaxLength(15).HasColumnName("profession");
            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            builder.Property(p => p.State).HasColumnName("state").HasConversion<string>().IsRequired();
        }
    }
}