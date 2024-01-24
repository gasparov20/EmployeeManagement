using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly? HireDate { get; set; }
    public DateOnly? BirthDate { get; set; }
    public int? Salary { get; set; }
    public string? Title { get; set; }
    public string? Department { get; set; }

    /// <summary>
    /// Updates this employee object's fields with new values
    /// </summary>
    /// <param name="employeeB">The employee object to copy non-null values from</param>
    public void CopyNonNullValuesFrom(Employee employeeB)
    {
        Employee employeeA = this;
        
        if (employeeB.Department != null)
            employeeA.Department = employeeB.Department;
        
        if (employeeB.FirstName != null)
            employeeA.FirstName = employeeB.FirstName;
        
        if (employeeB.LastName != null)
            employeeA.LastName = employeeB.LastName;
        
        if (employeeB.HireDate != null)
            employeeA.HireDate = employeeB.HireDate;
        
        if (employeeB.Salary != null)
            employeeA.Salary = employeeB.Salary;
        
        if (employeeB.Title != null)
            employeeA.Title = employeeB.Title;
        
        if (employeeB.BirthDate != null)
            employeeA.BirthDate = employeeB.BirthDate;
    }
}