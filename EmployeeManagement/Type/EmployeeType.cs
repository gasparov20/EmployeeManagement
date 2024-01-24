using EmployeeManagement.Models;
using GraphQL.Types;

namespace EmployeeManagement.Type;

public class EmployeeType : ObjectGraphType<Employee>
{
    public EmployeeType()
    {
        Field(e => e.Id);
        Field(e => e.FirstName);
        Field(e => e.LastName);
        Field(e => e.HireDate, true);
        Field(e => e.BirthDate, true);
        Field(e => e.Salary, true);
        Field(e => e.Title, true);
        Field(e => e.Department, true);
    }
}