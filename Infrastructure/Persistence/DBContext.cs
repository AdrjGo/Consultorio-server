using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Infrastructure.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Submodule> Submodules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Monitoring> Monitorings { get; set; }
        public DbSet<EvidenceFile> EvidenceFiles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientResponsible> PatientResponsibles { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<PaymentManager> PaymentManagers { get; set; }
        public DbSet<ClinicHistory> ClinicHistories { get; set; }
        public DbSet<GeneralHistory> GeneralHistories { get; set; }
        public DbSet<PretreatmentExam> PretreatmentExams { get; set; }
        public DbSet<TreatmentProgress> TreatmentProgress { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormRes> FormRes { get; set; }
        public DbSet<FormVersion> FormVersions { get; set; }
        public DbSet<TreatmentSummary> TreatmentSummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
        }
    }
}