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

            // Statistics
            modelBuilder.Entity<StatisticEntity>().ToTable("Statistics");

            /*modelBuilder.Entity<PlayerEntity>()
                        .HasOne(p => p.Statistics) 
                        .WithOne(s => s.Player)
                        .HasForeignKey<StatisticsEntity>(s => s.ID);*/

            /* Used to save the scores. Not implemented.
            var intArrayValueConverter = new ValueConverter<int[], string>(
                i => string.Join(",", i),
                s => string.IsNullOrWhiteSpace(s) ? new int[0] : s.Split(new[] { ',' }).Select(v => int.Parse(v)).ToArray());
            modelBuilder.Entity<StatisticsEntity>()
                .Property(e => e.Scores)
                .HasConversion(intArrayValueConverter);
            */

            string randomImagesWebsite = @"https://picsum.photos/400";

            var stats = new StatisticEntity[] {
                new StatisticEntity
                {
                    ID = 1,
                    BestScore = 170,
                    NumberOfDefeat = 5,
                    NumberOfVictory = 1,
                    NumberOfGames = 7,
                    //Scores = { 78, 101, 120, 170, 147, 98, 121 }
                },
                new StatisticEntity
                {
                    ID = 2,
                    BestScore = 184,
                    NumberOfDefeat = 1,
                    NumberOfVictory = 2,
                    NumberOfGames = 5,
                    //Scores = { 142, 89, 184, 75, 86 }
                },
                new StatisticEntity
                {
                    ID = 3,
                    BestScore = 0,
                    NumberOfDefeat = 0,
                    NumberOfVictory = 0,
                    NumberOfGames = 0,
                    //Scores = {}
                }
            };

            var players = new PlayerEntity[]
            {
                new PlayerEntity { ID = stats[0].ID, Name = "Mickael", Image = randomImagesWebsite/*, Statistics = stats[0]*/ },
                new PlayerEntity { ID = stats[1].ID, Name = "Jeremy", Image = randomImagesWebsite/*, Statistics = stats[1]*/ },
                new PlayerEntity { ID = stats[2].ID, Name = "Lucas", Image = randomImagesWebsite/*, Statistics = stats[2]*/ }
            };

            /*stats[0].Player = players[0];
            stats[1].Player = players[1];
            stats[2].Player = players[2];*/

            modelBuilder.Entity<StatisticEntity>().HasData(
                new StatisticEntity { ID = 1, BestScore = 42, NumberOfDefeat = 2, NumberOfGames = 5, NumberOfVictory = 3 },
                new StatisticEntity { ID = 2, BestScore = 23, NumberOfDefeat = 4, NumberOfGames = 7, NumberOfVictory = 2 },
                new StatisticEntity { ID = 3, BestScore = 65, NumberOfDefeat = 3, NumberOfGames = 15, NumberOfVictory = 12 }
            );

            modelBuilder.Entity<StatisticEntity>().HasData(stats);
            modelBuilder.Entity<PlayerEntity>().HasData(players);

            base.OnModelCreating(modelBuilder);
        }
    }
}
