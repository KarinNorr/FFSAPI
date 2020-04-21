using System;
using Microsoft.EntityFrameworkCore;
namespace FFSAPI.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trivia> Trivias { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Movie_Studio> MoviesInStudios { get; set; }
    }
}
