using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using postgresConnection.DBContext;
using postgresConnection.Models;

using Microsoft.EntityFrameworkCore.SqlServer;
    
class Program
{
    static void Main()
    {
        var config = ConfigurationRoot();

        var optionBuilder = new DbContextOptionsBuilder<AppDBContext>();
        optionBuilder.UseSqlServer(config.GetConnectionString("sql"));


        var context = new AppDBContext(optionBuilder.Options);
        // db.Database.EnsureCreated();  
        Console.WriteLine("Database created successfully!");

        Employee employee = new Employee
        {
            Name = "John Doe",
            Email = "john@gmail.com"
        };

        AddEmployee(context, employee);
        DeleteEmployee(context, 1);
        GetAllEmployes(context);
    }

    private static IConfigurationRoot ConfigurationRoot()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        return config;
    }

    private static void DeleteEmployee(AppDBContext context, int employeeId)
    {
        var employee = context.Employees.Find(employeeId);
        if (employee != null)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();
            Console.WriteLine("Employee deleted successfully!");
        }
        else
        {
            Console.WriteLine("Employee not found!");
        }
    }

    private static void AddEmployee(AppDBContext context, Employee employee)
    {
        context.Add(employee);
        context.SaveChanges();
        Console.WriteLine("Employee added successfully!");
    }

    private static void GetAllEmployes(AppDBContext context)
    {
        var employees = context.Employees.ToList();
        Console.WriteLine("Employees in the database:");
        foreach (var emp in employees)
        {
            Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Email: {emp.Email}");
        }
    }
}