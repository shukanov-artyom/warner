using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Warner.Persistency;

namespace Warner.Api.Migrations
{
    [DbContext(typeof(ApplicationDataContext))]
    [Migration("20170807105227_Add LogLineNumber column")]
    partial class AddLogLineNumbercolumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Warner.Api.Persistency.Entities.BuildEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("BuildDate");

                    b.Property<long>("BuildNumber");

                    b.Property<string>("LogFileName");

                    b.Property<long>("ProjectId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Build");
                });

            modelBuilder.Entity("Warner.Api.Persistency.Entities.BuildWarningEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BuildId");

                    b.Property<int>("CodeLineNumber");

                    b.Property<string>("DeveloperName");

                    b.Property<int>("LogLineNumber");

                    b.Property<string>("SourceFileName")
                        .IsRequired();

                    b.Property<string>("WarningType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BuildId");

                    b.ToTable("BuildWarning");
                });

            modelBuilder.Entity("Warner.Api.Persistency.Entities.ProjectEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Warner.Api.Persistency.Entities.BuildEntity", b =>
                {
                    b.HasOne("Warner.Api.Persistency.Entities.ProjectEntity", "Project")
                        .WithMany("Builds")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Warner.Api.Persistency.Entities.BuildWarningEntity", b =>
                {
                    b.HasOne("Warner.Api.Persistency.Entities.BuildEntity", "Build")
                        .WithMany("BuildWarnings")
                        .HasForeignKey("BuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
