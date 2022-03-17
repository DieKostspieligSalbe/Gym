﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL.DAL
{
    public class GeneralContext : DbContext
    {
        public GeneralContext()
        {
        }

        public GeneralContext(DbContextOptions<GeneralContext> options)
            : base(options)
        {
        }

        public DbSet<UserDAL> Users { get; set; } = null!;
        public DbSet<UserProfileDAL> UserProfiles { get; set; } = null!;
        public DbSet<TrainingProgramDAL> TrainingPrograms { get; set; }
        public DbSet<MuscleDAL> Muscles { get; set; }
        public DbSet<ExerciseDAL> Exercises { get; set; }
        public DbSet<MachineDAL> Machines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConnectionConfiguring();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDAL>()
                .HasMany(u => u.ProgramList)
                .WithMany(p => p.Users);

            modelBuilder.Entity<TrainingProgramDAL>()
                .HasMany(t => t.MuscleList)
                .WithMany(m => m.InvolvedInPrograms);

            modelBuilder.Entity<TrainingProgramDAL>()
                .HasMany(t => t.ExerciseList)
                .WithMany(e => e.UsedInPrograms);

            modelBuilder.Entity<MuscleDAL>()
                .HasMany(m => m.PrimaryExList)
                .WithMany(e => e.PrimaryMuscleList);

            modelBuilder.Entity<MuscleDAL>()
                .HasMany(m => m.SecondaryExList)
                .WithMany(e => e.SecondaryMuscleList);

            modelBuilder.Entity<ExerciseDAL>()
                .HasMany(e => e.MachineList)
                .WithMany(m => m.ExercisesList);

            modelBuilder.Entity<MachineDAL>()
                .HasMany(m => m.PrimaryMusclesList)
                .WithMany(m => m.PrimaryMachineList);

            modelBuilder.Entity<MachineDAL>()
                .HasMany(m => m.SecondaryMusclesList)
                .WithMany(m => m.SecondaryMachineList);

            modelBuilder.Entity<UserDAL>()
                .HasOne(u => u.UserProfile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfileDAL>(p => p.UserId);
        }

        protected string ConnectionConfiguring()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            return connectionString;

            //var optionBuilder = new DbContextOptionsBuilder<entitytestsdbContext>();
            //var options = optionBuilder.UseSqlServer(connectionString).Options;

        }


    }
}