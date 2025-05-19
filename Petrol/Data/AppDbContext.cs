using Microsoft.EntityFrameworkCore;
using Petrol.Models;

namespace Petrol.Data 
{
    public class AppDbContext : DbContext 
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Models.Program> Programs { get; set; }
        public DbSet<Training> Training { get; set; }
        public DbSet<EmployeeTraining> EmployeeTraining { get; set; }
        public DbSet<FollowingReport> FollowingReports { get; set; }
        public DbSet<DepartmentPresenceNumber> DepartmentPresenceNumbers { get; set; }
        public DbSet<ProgramType> ProgramTypes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-COOAS6G;Initial Catalog=PetrolTraining;User Id=sa;Password=12345678");
        }

    }
}