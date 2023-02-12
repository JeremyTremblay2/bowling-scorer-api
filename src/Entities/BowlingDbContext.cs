using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;


namespace Entities
{
    /// <summary>
    /// Represent a DBcontext class for our BowlingScorer app.
    /// </summary>
    public class BowlingDbContext : DbContext
    {
        /// <summary>
        /// Contains a set of PlayerEntity which represents the players contained in our application.
        /// </summary>
        public DbSet<PlayerEntity>? Players { get; set; }

        public DbSet<StatisticEntity>? Statistics { get; set; }

        public BowlingDbContext()
        { }

        public BowlingDbContext(DbContextOptions<BowlingDbContext> options)
            : base(options)
        { }

        /// <summary>
        /// Used to configure the database.
        /// </summary>
        /// <param name="optionsBuilder">The options that can be configured.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=Entities.BowlingDatabase.db");
            }
        }

        /// <summary>
        /// Method call when the model is creating, and allow us to define some settings and constraints 
        /// about our entities before creating them.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Players
            modelBuilder.Entity<PlayerEntity>().ToTable("Player");
            modelBuilder.Entity<PlayerEntity>().Property(n => n.Name)
                                               .IsRequired()
                                               .HasMaxLength(256);
            modelBuilder.Entity<PlayerEntity>().Property(n => n.ID)
                                               .ValueGeneratedOnAdd();

            modelBuilder.Entity<PlayerEntity>().HasData(
                new PlayerEntity { ID = 1, Name = "Mickael", Image = "imageMickael.png" },
                new PlayerEntity { ID = 2, Name = "Jeremy", Image = "imageJeremy.png" },
                new PlayerEntity { ID = 3, Name = "Lucas", Image = "imageLucas.png" }
            );

            // Statistics
            modelBuilder.Entity<StatisticEntity>().ToTable("Statistics");

            modelBuilder.Entity<StatisticEntity>().HasData(
                new StatisticEntity { ID = 1, BestScore = 42, NumberOfDefeat = 2, NumberOfGames = 5, NumberOfVictory = 3 },
                new StatisticEntity { ID = 2, BestScore = 23, NumberOfDefeat = 4, NumberOfGames = 7, NumberOfVictory = 2 },
                new StatisticEntity { ID = 3, BestScore = 65, NumberOfDefeat = 3, NumberOfGames = 15, NumberOfVictory = 12 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
