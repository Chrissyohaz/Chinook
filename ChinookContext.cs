using System;
using Microsoft.Data.Sqlite;
using ChinookEntities;
using Microsoft.EntityFrameworkCore;

namespace ChinookContext
{
    public class ChinookDatabase : DbContext
    {
        public DbSet<Album> albums { get; set; }
        public DbSet<Artist> artists { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
            .HasOne(al => al.artists)
            .WithMany(ar => ar.albums)
            .HasForeignKey(al => al.ArtistId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "chinook.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }

    // public class Album
    // {
    //     public int AlbumId { get; set; }
    //     public string Title { get; set; }
    //     public int ArtistId { get; set; }
    // }
}
