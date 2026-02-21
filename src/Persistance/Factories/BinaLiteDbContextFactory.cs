using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance.Factories;

public class BinaLiteDbContextFactory : IDesignTimeDbContextFactory<BinaLiteDbContext>
{
    public BinaLiteDbContext CreateDbContext(string[] args)
    {
        // API layihəsinin path-i
        var apiProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "API");
        // appsettings.json-dən connection string oxu
        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiProjectPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<BinaLiteDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new BinaLiteDbContext(optionsBuilder.Options);
    }
}
