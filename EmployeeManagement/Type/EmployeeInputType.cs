using GraphQL.Types;

namespace EmployeeManagement.Type;

public class EmployeeInputType : InputObjectGraphType
{
    public EmployeeInputType()
    {
        Field<StringGraphType>("firstName");
        Field<StringGraphType>("lastName");
        Field<DateOnlyGraphType>("hireDate");
        Field<DateOnlyGraphType>("birthDate");
        Field<IntGraphType>("salary");
        Field<StringGraphType>("title");
        Field<StringGraphType>("department");
    }
}