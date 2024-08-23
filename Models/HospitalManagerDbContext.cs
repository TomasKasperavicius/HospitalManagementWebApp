using Microsoft.EntityFrameworkCore;

namespace HospitalManagementWebApp.Models
{
    public class HospitalManagerDbContext : DbContext
    {
        public HospitalManagerDbContext(DbContextOptions<HospitalManagerDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> patients { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<WorkSchedule> workSchedules { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasOne<Address>()
                .WithMany()
                .HasForeignKey(d => d.AddressID);

            modelBuilder.Entity<WorkSchedule>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(ws => ws.DoctorID);

            modelBuilder.Entity<Appointment>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(a => a.DoctorID);
            modelBuilder.Entity<Appointment>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(a => a.UserID);
            modelBuilder.Entity<Appointment>()
                .HasOne<Address>()
                .WithMany()
                .HasForeignKey(a => a.AddressID);
        }

    }
}
