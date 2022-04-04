using System;
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
        public DbSet<TrainingProgramDAL> TrainingPrograms { get; set; }
        public DbSet<MuscleDAL> Muscles { get; set; }
        public DbSet<ExerciseDAL> Exercises { get; set; }
        public DbSet<EquipDAL> Equipment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConnectionConfiguring();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //add here mediator table names
        {
            modelBuilder.Entity<UserDAL>()
                .HasMany(u => u.ProgramList)
                .WithMany(p => p.Users);

            modelBuilder.Entity<TrainingProgramDAL>()
                .HasMany(t => t.MuscleList);//

            modelBuilder.Entity<TrainingProgramDAL>()
                .HasMany(t => t.ExerciseList);//

            modelBuilder.Entity<MuscleDAL>()
                .HasMany(m => m.PrimaryExList)
                .WithMany(e => e.PrimaryMuscleList);

            modelBuilder.Entity<MuscleDAL>()
                .HasMany(m => m.SecondaryExList)
                .WithMany(e => e.SecondaryMuscleList);

            modelBuilder.Entity<ExerciseDAL>()
                .HasMany(e => e.EquipList)
                .WithMany(m => m.ExercisesList);

            modelBuilder.Entity<EquipDAL>()
                .HasMany(m => m.PrimaryMusclesList);//

            modelBuilder.Entity<EquipDAL>()
                .HasMany(m => m.SecondaryMusclesList);//

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