using System.ComponentModel;
using EmployeeManagement.Database;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using GraphQL;

namespace EmployeeManagement.Services;

public enum FilterField
{
    Title,
    Department,
    SalaryRange
}

public enum SortField
{
    HireDate,
    Salary
}

public class EmployeeRepository(GraphQlDbContext dbContext) : IEmployeeRepository
{
    private GraphQlDbContext _dbContext = dbContext;

    public List<Employee>? GetAllEmployees()
    {
        return _dbContext.Employees?.ToList();
    }
    
    public List<Employee>? GetAllEmployeesFiltered(FilterField? field, string argument)
    {
        switch (field)
        {
            case FilterField.Title:
                return _dbContext.Employees?.Where(employee => employee.Title == argument).ToList();
            case FilterField.Department:
                return _dbContext.Employees?.Where(employee => employee.Department == argument).ToList();
            case FilterField.SalaryRange:
                try
                {
                    var minMaxSalary = argument.Split('-');
                    var minSalary = int.Parse(minMaxSalary[0]);
                    var maxSalary = int.Parse(minMaxSalary[1]);
                    return _dbContext.Employees?
                        .Where(employee => employee.Salary >= minSalary && employee.Salary <= maxSalary).ToList();
                }
                catch (Exception e) when (e is FormatException or IndexOutOfRangeException)
                {
                    throw new ExecutionError(
                        "Invalid salary range argument. Salary range must only contain a number" +
                        "followed by a dash followed by a number. e.g. 10000-40000");
                }
            default:
                return null;
        }
    }
    
    public List<Employee>? GetAllEmployeesSorted(SortField sortField, ListSortDirection direction)
    {
        var sortedList = _dbContext.Employees?.ToList();

        if (sortedList == null)
            return null;
        
        sortedList.Sort(delegate(Employee x, Employee y)
        {
            var sortFactor = direction == ListSortDirection.Ascending ? 1 : -1;
            return sortField switch
            {
                SortField.Salary => x.Salary >= y.Salary ? sortFactor : sortFactor * -1,
                SortField.HireDate => x.HireDate >= y.HireDate ? sortFactor : sortFactor * -1,
                _ => 0
            };
        });

        return sortedList;
    }

    public Employee? GetEmployeeById(int id)
    {
        return _dbContext.Employees?.FirstOrDefault(employee => employee.Id == id);
    }

    public List<Employee>? GetEmployeesByName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            throw new ExecutionError($"Please input a first name and/or last name.");

        if (string.IsNullOrEmpty(lastName))
            return _dbContext.Employees?.Where(employee => employee.FirstName == firstName).ToList();
        if (string.IsNullOrEmpty(firstName))
            return _dbContext.Employees?.Where(employee => employee.LastName == lastName).ToList();
        
        return _dbContext.Employees?
            .Where(employee => employee.FirstName == firstName && employee.LastName == lastName).ToList();
    }

    public Employee? UpdateEmployee(int id, Employee employeeUpdatedFields)
    {
        var employeeToUpdate = _dbContext.Employees?.FirstOrDefault(employee => employee.Id == id);

        if (employeeToUpdate == null)
            throw new ExecutionError($"Employee with ID: {id} does not exist and could not be updated.");

        employeeToUpdate.CopyNonNullValuesFrom(employeeUpdatedFields);
        _dbContext.SaveChanges();
        
        return employeeToUpdate;
    }

    public Employee AddEmployee(Employee newEmployee)
    {
        try
        {
            _dbContext.Employees?.Add(newEmployee);
            _dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return newEmployee;
    }

    public string DeleteEmployee(int id)
    {
        try
        {
            var employee = _dbContext.Employees?.ToList().FirstOrDefault(employee => employee.Id == id);
        
            if (employee != null)
            {
                _dbContext.Employees?.Remove(employee);
                _dbContext.SaveChanges();
                return $"{id}: {employee.LastName}, {employee.FirstName} was successfully deleted.";
            }
        }
        catch (Exception e)
        {
            return $"Employee with ID: {id} could not be deleted.{Environment.NewLine}Error: {e.GetBaseException().Message}";
        }

        return $"Employee with ID: {id} does not exist and could not be deleted.";
    }
}