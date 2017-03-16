using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectManager.Data;

namespace ProjectManager.Migrations
{
    [DbContext(typeof(ProjectManagerContext))]
    [Migration("20170316045206_RemoveReleaseDate")]
    partial class RemoveReleaseDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("ProjectManager.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManager.Entities.UserStory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Completed");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("ProjectId");

                    b.Property<string>("WorkRemaining");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("UserStories");
                });

            modelBuilder.Entity("ProjectManager.Entities.UserStory", b =>
                {
                    b.HasOne("ProjectManager.Entities.Project", "Project")
                        .WithMany("UserStories")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
