using System.ComponentModel;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Services;
using EmployeeManagement.Type;
using GraphQL;
using GraphQL.Types;

namespace EmployeeManagement.Query;

public class EmployeeQuery : ObjectGraphType
{
    public EmployeeQuery(IEmployeeRepository employeeRepository)
    {
        Field<ListGraphType<EmployeeType>>("GetAllEmployees")
            .Resolve(_ => employeeRepository.GetAllEmployees());

        Field<EmployeeType>("GetEmployeeById")
            .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "employeeId" })).Resolve(
                context => employeeRepository.GetEmployeeById(context.GetArgument<int>("employeeId")));
        
        Field<ListGraphType<EmployeeType>>("GetEmployeesByName")
            .Arguments(new QueryArguments(new QueryArgument<StringGraphType> { Name = "firstName" },
                new QueryArgument<StringGraphType> { Name = "lastName" })).Resolve(
                context =>
                {
                    var firstName = context.GetArgument<string>("firstName");
                    var lastName = context.GetArgument<string>("lastName");
                    return employeeRepository.GetEmployeesByName(firstName, lastName);
                });

        Field<ListGraphType<EmployeeType>>("GetAllEmployeesFiltered")
            .Arguments(new QueryArguments(new QueryArgument<EnumerationGraphType<FilterField>> { Name = "filterField" },
                new QueryArgument<StringGraphType> { Name = "filterString" }))
            .Resolve(context =>
            {
                // Meaningful error messages
                var bMissingFilterField =
                    context.Arguments?["filterField"].Value.GetPropertyValue(typeof(FilterField)) == null;
                var bMissingFilterString =
                    context.Arguments?["filterString"].Value.GetPropertyValue(typeof(string)) == null;
                if (bMissingFilterField && bMissingFilterString)
                    throw new ExecutionError($"Query failed due to missing input arguments: filterField, filterString");
                if (bMissingFilterField)
                    throw new ExecutionError($"Query failed due to missing input arguments: filterField");
                if (bMissingFilterString)
                    throw new ExecutionError($"Query failed due to missing input arguments: filterString");
                
                var filterField = context.GetArgument<FilterField>("filterField");
                var filterString = context.GetArgument<string>("filterString");
                return employeeRepository.GetAllEmployeesFiltered(filterField, filterString);
            });
        
        Field<ListGraphType<EmployeeType>>("GetAllEmployeesSorted")
            .Arguments(new QueryArguments(new QueryArgument<EnumerationGraphType<SortField>> { Name = "orderBy" },
                new QueryArgument<EnumerationGraphType<ListSortDirection>> { Name = "direction" }))
            .Resolve(context =>
            {
                // Meaningful error messages
                var bMissingOrderBy =
                    context.Arguments?["orderBy"].Value.GetPropertyValue(typeof(SortField)) == null;
                var bMissingDirection =
                    context.Arguments?["direction"].Value.GetPropertyValue(typeof(ListSortDirection)) == null;
                if (bMissingOrderBy && bMissingDirection)
                    throw new ExecutionError($"Query failed due to missing input arguments: orderBy, direction");
                if (bMissingOrderBy)
                    throw new ExecutionError($"Query failed due to missing input arguments: orderBy");
                if (bMissingDirection)
                    throw new ExecutionError($"Query failed due to missing input arguments: direction");
                
                var orderBy = context.GetArgument<SortField>("orderBy");
                var direction = context.GetArgument<ListSortDirection>("direction");
                return employeeRepository.GetAllEmployeesSorted(orderBy, direction);
            });
    }
}