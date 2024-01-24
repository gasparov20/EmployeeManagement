using System.ComponentModel;
using EmployeeManagement.Models;
using EmployeeManagement.Services;

namespace EmployeeManagement.Interfaces;

/*
 * User Stories
   1. As a user I can query all the employees within the organization
   2. As a user I am able to sort the employees by date of joining and
   salary
   3. As a user, I’m able to filter the employees list by their title,
   department, and range of salary
   4. As a user, I can query details of any employee
   5. As a user, I can update details of any employee
   6. As a user, I can add a new employee
   7. As a user, I can delete any employee
   8. Employee properties should include: id, first name, last name,
   date of joining, date of birth, salary, title, department
 */

public interface IEmployeeRepository
{
    List<Employee>? GetAllEmployees();
    List<Employee>? GetAllEmployeesFiltered(FilterField? field, string argument);
    List<Employee>? GetAllEmployeesSorted(SortField field, ListSortDirection direction);
    Employee? GetEmployeeById(int id);
    List<Employee>? GetEmployeesByName(string firstName, string lastName);
    Employee? UpdateEmployee(int id, Employee employee);
    Employee? AddEmployee(Employee newEmployee);
    string DeleteEmployee(int id);
}