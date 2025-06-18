using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using postgresConnection.DBContext;
using postgresConnection.Models;

class Program
{
    static void Main()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var optionBuilder = new DbContextOptionsBuilder<AppDBContext>();
        optionBuilder.UseNpgsql(config.GetConnectionString("DBConnection"));


        var context = new AppDBContext(optionBuilder.Options);
        // db.Database.EnsureCreated();  
        Console.WriteLine("Database created successfully!");

        Employee employee = new Employee
        {
            Name = "John Doe",
            Email = "john@gmail.com"
        };

        context.Add(employee);
        context.SaveChanges();
        Console.WriteLine("Employee added successfully!");


        var employees = context.Employees.ToList();
        Console.WriteLine("Employees in the database:");
        foreach (var emp in employees)
        {
            Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Email: {emp.Email}");
        }
    }
}