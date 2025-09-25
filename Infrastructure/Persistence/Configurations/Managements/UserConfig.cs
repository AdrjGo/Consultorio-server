using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(u => u.Id).HasName("user_id");
            builder.Property(u => u.Id).HasColumnName("user_id").IsRequired();
            builder.Property(u => u.PersonId).HasColumnName("person_id").IsRequired();
            builder.Property(u => u.Password).HasColumnName("password").IsRequired();

            builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
            builder.Property(p => p.UpdatedBy).HasColumnName("updated_by");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at");
            builder.Property(p => p.State).HasColumnName("state").HasConversion<string>().IsRequired();

            builder.HasOne(u => u.Person)
                   .WithOne(p => p.User)
                   .HasForeignKey<User>(u => u.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}