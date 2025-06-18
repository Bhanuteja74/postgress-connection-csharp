using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using postgresConnection.Models;

namespace postgresConnection.DBContext;


public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDBContext>
{
    public AppDBContext CreateDbContext(string[] args)
    {
        // Load configuration from appsettings.json
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // important
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = config.GetConnectionString("DBConnection");

        var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDBContext(optionsBuilder.Options);
    }
}

public class AppDBContext  : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public AppDBContext()
    {
    }
    
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }
    
}