using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PS.Persistence.DataContext;


public class DBContext : DbContext
{

    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
        
    }


    public DbSet<Country>? Countries { get; set; }
    public DbSet<TaxBracket>? TaxBrackets { get; set; }
    public DbSet<TaxBracketLine>? TaxBracketLines { get; set; }
    public DbSet<TaxCalculation>? TaxCalculations { get; set; }
    public DbSet<TaxRate>? TaxRates { get; set; }


    public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        DBContext IDesignTimeDbContextFactory<DBContext>.CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var connection = configuration.GetConnectionString("PS");
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer(connection);

            return new DBContext(optionsBuilder.Options);
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
    }



}

