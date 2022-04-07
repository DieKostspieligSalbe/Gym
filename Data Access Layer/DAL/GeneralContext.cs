using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Gym.DAL.Models;

namespace Gym.DAL
{
    public class GeneralContext : DbContext
    {
        public GeneralContext()
        {
            Database.EnsureCreated();
        }

        public GeneralContext(DbContextOptions<GeneralContext> options)
            : base(options)
        {
        }

        public DbSet<UserDAL> Users { get; set; } = null!;
        public DbSet<TrainingProgramDAL> TrainingPrograms { get; set; }
        public DbSet<MuscleDAL> Muscles { get; set; }
        public DbSet<ExerciseDAL> Exercises { get; set; }
        public DbSet<EquipDAL> Equipment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConnectionConfiguring();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Gym"));
            }
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //add here mediator table names
        {
            modelBuilder.Entity<UserDAL>()
                .HasMany(u => u.ProgramList)
                .WithMany(p => p.Users)
                .UsingEntity(j => j.ToTable("UsersPrograms"));

            modelBuilder.Entity<TrainingProgramDAL>()
                .HasMany(t => t.MuscleList)
                .WithMany(m => m.InvolvedInPrograms)
                .UsingEntity(j => j.ToTable("MusclesPrograms"));


            modelBuilder.Entity<TrainingProgramDAL>()
                .HasMany(t => t.ExerciseList)
                .WithMany(e => e.UsedInPrograms)
                .UsingEntity(j => j.ToTable("ExercisesPrograms"));

            modelBuilder.Entity<ExerciseDAL>()
                .HasMany(e => e.EquipList)
                .WithMany(eq => eq.ExercisesList)
                .UsingEntity(j => j.ToTable("ExercisesEquip"));

            modelBuilder.Entity<ExerciseDAL>()
                .HasMany(e => e.PrimaryMuscleList)
                .WithMany(m => m.PrimaryExList)
                .UsingEntity(j => j.ToTable("ExerciseMusclePrimary"));

            modelBuilder.Entity<ExerciseDAL>()
                .HasMany(e => e.SecondaryMuscleList)
                .WithMany(m => m.SecondaryExList)
                .UsingEntity(j => j.ToTable("ExerciseMuscleSecondary"));

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