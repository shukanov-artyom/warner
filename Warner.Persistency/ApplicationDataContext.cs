using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Warner.Persistency.Entities;
using Warner.Persistency.Options;

namespace Warner.Persistency
{
    public class ApplicationDataContext : DbContext
    {
        private readonly DatabaseOptions databaseOptions;
        private readonly string connectionString;

        public ApplicationDataContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ApplicationDataContext(IOptions<DatabaseOptions> databaseOptions)
        {
            this.databaseOptions = databaseOptions.Value;
        }

        public DbSet<ProjectEntity> Projects { get; set; }

        public DbSet<BuildEntity> Builds { get; set; }

        public DbSet<BuildWarningEntity> Warnings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>().ToTable("Project");
            modelBuilder.Entity<BuildEntity>().ToTable("Build");
            modelBuilder.Entity<BuildWarningEntity>().ToTable("BuildWarning");
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string selectedConnectionString;
            if (databaseOptions != null)
            {
                selectedConnectionString = databaseOptions.WarnerDatabase;
            }
            else
            {
                selectedConnectionString = connectionString;
            }
            optionsBuilder.UseSqlServer(selectedConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
