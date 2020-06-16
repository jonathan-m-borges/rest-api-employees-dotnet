using Microsoft.EntityFrameworkCore;
using RestApiEmployees.Domain.Models;

namespace RestApiEmployees.Persistence.PostgreEF
{
    public class EmployeesDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeesDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .ToTable("employees")
                .Property(nameof(Employee.ProfileImage))
                .HasColumnName("profile_image");
        }
    }
}