using MetroProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Npgsql.EntityFrameworkCore.PostgreSQL; // Add this using directive at the top of the file
using System.Threading.Tasks;

namespace MetroProject.Domain
{
    public class AppDbContext : DbContext
    {
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        

        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your actual PostgreSQL connection string
            optionsBuilder.UseNpgsql("Host=db;Database=metrodb;Username=postgres;Password=yourpassword");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and table mappings if needed
        }
    }
}
