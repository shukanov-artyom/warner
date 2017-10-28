using System;
using Microsoft.EntityFrameworkCore;
using Warner.Persistency.Entities;

namespace Warner.Persistency
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(
            DbContextOptions<ApplicationDataContext> databaseOptions)
            : base(databaseOptions)
        {
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
    }
}
