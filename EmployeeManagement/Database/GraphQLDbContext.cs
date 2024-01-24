using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Database;

public class GraphQlDbContext : DbContext
{
    public GraphQlDbContext(DbContextOptions<GraphQlDbContext> options) : base(options)
    {
        
    }

    public DbSet<Employee>? Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // modelBuilder.Entity<Employee>().HasData(
        //     new Employee() { FirstName = "Andrew", LastName = "Smith", BirthDate = new DateOnly(1994, 7, 27), HireDate = new DateOnly(2024, 1, 25), Department = "Engineering", Salary = 110000, Title = "Software Engineer", Id = 1}
        // );
        
        modelBuilder.Entity<Employee>().Property(employee => employee.Id).ValueGeneratedOnAdd();
    }
}