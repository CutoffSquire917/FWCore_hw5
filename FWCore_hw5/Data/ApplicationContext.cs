using FWCore_hw5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore_hw5.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=FWCore_hw5;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Genre>(builder =>
            {
                builder.HasMany(e => e.Books).WithOne(e => e.Genre).OnDelete(DeleteBehavior.Restrict);
                builder.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Author>(builder =>
            {
                builder.HasMany(e => e.Books).WithOne(e => e.Author).OnDelete(DeleteBehavior.Restrict);
                builder.HasIndex(e => e.Email).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
