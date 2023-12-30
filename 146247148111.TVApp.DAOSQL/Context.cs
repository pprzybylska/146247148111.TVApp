using _146247.TVApp.DAOSQL;
using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace _146247148111.TVApp.DAOSQL
{
    public class Context: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            optionsBuilder.UseSqlite($"Data source={basePath}/tvapp_db.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TV>()
                .HasOne(t => (Producer)t.Producer)
                .WithMany()
                .HasForeignKey(t => t.ProducerId);
        }

        public virtual DbSet<Producer> producers { get; set; }
        public virtual DbSet<TV> TVs { get; set; }

    }
}