using EmployeeManagement.Mutation;
using EmployeeManagement.Query;

namespace EmployeeManagement.Schema;

public class EmployeeSchema : GraphQL.Types.Schema
{
    public EmployeeSchema(EmployeeQuery employeeQuery, EmployeeMutation employeeMutation)
    {
        Query = employeeQuery;
        Mutation = employeeMutation;
    }
}