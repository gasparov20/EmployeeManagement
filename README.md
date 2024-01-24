# EmployeeManagement GraphQL API
This was developed for a take home assessment for CoverTree. I chose to use C#.NET for this project because it's the language I use in my current role, and I chose GraphQL (despite the fact that it's way overkill) because I wanted to get familiar with it if I'm hired. I also was very interested in the query syntax and flexibiity of getting precisely the data that is needed for a given task. The database is a Microsoft SQL database.

## How to use
I've deployed the .NET server and GraphiQL interface to Azure for easy demonstration.  
If you are authorized, I will have sent you a link.

The queries and mutations available can be explored by clicking "Docs" in the upper-right corner. The database is a simple design, with the root being the list of all of the "employees" in the database.

To interact with the API, check out the available fields and subfields and input your queries/mutations on the left side of the screen. Feel free to give out promotions, raises, terminations, etc.

## Data Model
- **id: Int!**  
  Each employee has an ID, which is the primary key of the database.  
  These are auto-generated and cannot be changed.

- **firstName: String!**  
  First name cannot result in a null value.

- **lastName: String!**  
  Last name cannot result in a null value.

- **hireDate: DateOnly**  
  Hire date cannot result in a null value.
  
- **birthDate: DateOnly**  
  Birth date cannot result in a null value.
  
- **salary: Int**  
  Salary can result in a null value.
  
- **title: String**  
  Title can result in a null value.
  
- **department: String**  
  Department can result in a null value.
  
## Queries
- **getAllEmployees: [EmployeeType]**  
  Returns a list of all of the employees in the database.

- **getEmployeeById(employeeId: Int): EmployeeType**  
  Returns the employee with the ID provided as an argument. Returns null if not found.

- **getEmployeesByName(firstName: String, lastName: String): [EmployeeType]**  
  Returns a list of employees with a matching name. Employees can be queried by their first names, last names, or first and last names. This was not a user story, but I felt it would be useful.

- **getAllEmployeesFiltered(filterField: FilterField, filterString: String): [EmployeeType]**  
  Allows filtering the database for employees based on title, department, and salary range. I implemented filtering this way because the framework I used (GraphQL for .NET) doesn't support complex filtering like other frameworks, such as Hot Chocolate. I provided the ability to switch the filtered field with an enumeration embedded in the server.  
  **Note:** To filter by salary range, the filterString must be a number, followed by a dash, followed by another number.  
  e.g. 10000-60000.
  
- **getAllEmployeesSorted(orderBy: SortField, direction: ListSortDirection): [EmployeeType]**  
  Allows sorting the database by hire date and salary. Similarly to filtering employees, I used enumerations for the arguments. Sorting can be done in ascending and descending order, and both arguments are required.

## Mutations
- **addEmployee(employee: EmployeeInputType): EmployeeType**  
  Use this mutation to add an employee. You can use the variables panel at the bottom of the query editor to create a JSON representation of an Employee object and then reference it in the query editor. To reference a defined variable, the identifier should be preceeded with a '$'. Returns the newly added employee.

- **updateEmployee(employeeId: Intemployee: EmployeeInputType): EmployeeType**  
  This allows you to update the fields of an employee in the database. Use a variable like how you would add an employee, but only include the fields that you want to update. Excluded fields will be ignored and the original values will be maintained. The newly updated employee will be returned. Employee ID cannot be changed and it is unrecognized as an input.

- **deleteEmployee(employeeId: Int): String**  
  This mutation will delete an employee from the database by their ID. No subfields can be queried here, and a string will be returned informing you of success or failure.
