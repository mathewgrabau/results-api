using CyclingResults.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace CyclingResults.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Without this constructor getting no provider error on dotnet ef migrations add InitialCreate
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Race> Races { get; set; }
    }
}
