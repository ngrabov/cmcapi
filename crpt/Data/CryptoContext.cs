using Microsoft.EntityFrameworkCore;
using crpt.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace crpt.Data
{
    public class CryptoContext : IdentityDbContext<Writer, IdentityRole<int>, int>
    {
        public CryptoContext(DbContextOptions<CryptoContext> options) : base(options)
        {
        }

        public DbSet<Datum> Data { get; set; }
        public DbSet<Writer> Writers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Datum>().Ignore(c => c.tags);
            modelBuilder.Entity<Datum>().Ignore(c => c.Quote);
            modelBuilder.Entity<Datum>().ToTable("Datum");
            modelBuilder.Entity<Writer>().ToTable("Writers");
        }
    }
}