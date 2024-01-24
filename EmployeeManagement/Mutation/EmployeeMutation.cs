using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Type;
using GraphQL;
using GraphQL.Types;

namespace EmployeeManagement.Mutation;

public class EmployeeMutation : ObjectGraphType
{
    public EmployeeMutation(IEmployeeRepository employeeRepository)
    {
        // Add Employee
        Field<EmployeeType>("AddEmployee")
            .Arguments(new QueryArguments(new QueryArgument<EmployeeInputType> { Name = "employee" })).Resolve(context =>
            {
                var employee = context.GetArgument<Employee>("employee");
                return employeeRepository.AddEmployee(employee);
            });
        
        // Update Employee
        Field<EmployeeType>("UpdateEmployee")
            .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "employeeId" },
                new QueryArgument<EmployeeInputType> { Name = "employee" })).Resolve(context =>
            {
                var employeeId = context.GetArgument<int>("employeeId");
                var employee = context.GetArgument<Employee>("employee");
                return employeeRepository.UpdateEmployee(employeeId, employee);
            });
        
        // Delete Employee
        Field<StringGraphType>("DeleteEmployee")
            .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "employeeId" })).Resolve(context =>
            {
                var employeeId = context.GetArgument<int>("employeeId");
                return employeeRepository.DeleteEmployee(employeeId);
            });
    }
}