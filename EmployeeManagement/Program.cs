using EmployeeManagement.Database;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Mutation;
using EmployeeManagement.Query;
using EmployeeManagement.Schema;
using EmployeeManagement.Services;
using EmployeeManagement.Type;
using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddTransient<EmployeeType>();
builder.Services.AddTransient<EmployeeQuery>();
builder.Services.AddTransient<EmployeeInputType>();
builder.Services.AddTransient<EmployeeMutation>();
builder.Services.AddTransient<ISchema, EmployeeSchema>();

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

builder.Services.AddDbContext<GraphQlDbContext>(option => 
    option.UseSqlServer(config["GraphQLDbConnection"]));

builder.Services.AddGraphQL(g => g.AddAutoSchema<ISchema>().AddSystemTextJson());

var app = builder.Build();

app.UseHttpsRedirection();

app.UseGraphiQl("/graphql");

app.UseGraphQL<ISchema>();

app.UseAuthorization();

app.MapControllers();

app.Run();