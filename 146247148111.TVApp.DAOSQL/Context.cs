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
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            string solutionPath = Path.GetFullPath(Path.Combine(baseDirectory, @"..\"));
            string dbPath = Path.Combine(solutionPath, "tvapp_db.db");
            optionsBuilder.UseSqlite($"Data source={dbPath}");
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