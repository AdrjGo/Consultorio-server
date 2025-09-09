using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Infrastructure.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRol> UserRol { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Monitoring> Monitoring { get; set; }
        public DbSet<EvidenceFile> EvidenceFile { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<PatientResponsible> PatientResponsible { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
        }
    }
}