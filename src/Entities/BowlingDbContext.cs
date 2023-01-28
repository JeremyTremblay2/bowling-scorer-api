using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Xml.Linq;


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

        public DbSet<StatisticsEntity>? Statistics { get; set; }

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
            base.OnModelCreating(modelBuilder);

            //Players
            modelBuilder.Entity<PlayerEntity>().ToTable("Player");
            modelBuilder.Entity<PlayerEntity>().Property(n => n.Name)
                                               .IsRequired()
                                               .HasMaxLength(256);
            modelBuilder.Entity<PlayerEntity>().Property(n => n.ID)
                                               .ValueGeneratedOnAdd();

            modelBuilder.Entity<PlayerEntity>()
                        .HasOne(n => n.Statistics) 
                        .WithOne(c => c.Player)
                        .HasForeignKey<StatisticsEntity>(c => c.ID);

            var intArrayValueConverter = new ValueConverter<int[], string>(
                    i => string.Join(",", i),
                    s => string.IsNullOrWhiteSpace(s) ? new int[0] : s.Split(new[] { ',' }).Select(v => int.Parse(v)).ToArray());

            /*modelBuilder.Entity<StatisticsEntity>()
                        .Property(e => e.Scores)
                        .HasConversion(intArrayValueConverter);*/

            modelBuilder.Entity<PlayerEntity>().HasData(
                new PlayerEntity { ID = 1, Name = "Mickael", Image = "imageMickael.png" },
                new PlayerEntity { ID = 2, Name = "Jeremy", Image = "imageJeremy.png" },
                new PlayerEntity { ID = 3, Name = "Lucas", Image = "imageLucas.png" }
            );

            modelBuilder.Entity<StatisticsEntity>().HasData(
                new StatisticsEntity
                {
                    ID = 1,
                    BestScore = 170,
                    NumberOfDefeat = 5,
                    NumberOfVictory = 1,
                    NumberOfGames = 7,
                    //Scores = { 78, 101, 120, 170, 147, 98, 121 }
                },
                new StatisticsEntity
                {
                    ID = 2,
                    BestScore = 184,
                    NumberOfDefeat = 1,
                    NumberOfVictory = 2,
                    NumberOfGames = 5,
                    //Scores = { 142, 89, 184, 75, 86 }
                },
                new StatisticsEntity
                {
                    ID = 3,
                    BestScore = 0,
                    NumberOfDefeat = 0,
                    NumberOfVictory = 0,
                    NumberOfGames = 0,
                    //Scores = {}
                }
            );

            

            
        }
    }
}

