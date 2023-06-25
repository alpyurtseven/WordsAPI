using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsAPI.Core.Models;

namespace WordsAPI.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<English> EnglishWords { get; set; }
        public DbSet<Turkish> TurkishWords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<English>()
                    .HasMany<Turkish>(e => e.Translations)
                    .WithMany(t => t.Translations)
                    .UsingEntity(j => j.ToTable("EnglishTurkishTranslations"));

           modelBuilder.Entity<English>()
                    .HasIndex(e => e.NormalizedWord)
                    .IsUnique();

           modelBuilder.Entity<Turkish>()
                    .HasIndex(e => e.NormalizedWord)
                    .IsUnique();

           modelBuilder.Entity<Category>()
                    .HasIndex(c => c.Name)
                    .IsUnique();

            modelBuilder.Entity<User>()
                    .HasIndex(u => u.UserName)
                    .IsUnique();

            modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
