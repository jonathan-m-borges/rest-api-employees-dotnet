using Microsoft.EntityFrameworkCore;
using RestApiEmployees.Domain.Models;

namespace RestApiEmployees.Persistence.PostgreEF
{
    public class EmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeesContext(DbContextOptions options) : base(options)
        {
        }

        protected EmployeesContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<Employee>().ToTable("employees");
            modelBuilder.Entity<Employee>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Employee>().Property(x => x.Name).HasColumnName("name");
            modelBuilder.Entity<Employee>().Property(x => x.Salary).HasColumnName("salary");
            modelBuilder.Entity<Employee>().Property(x => x.Age).HasColumnName("age");
            modelBuilder.Entity<Employee>().Property(x => x.ProfileImage).HasColumnName("profile_image");
        }
    }
}