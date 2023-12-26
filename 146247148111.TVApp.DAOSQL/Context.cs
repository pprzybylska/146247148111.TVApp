using _146247148111.TVApp.Core;
using _146247148111.TVApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _146247148111.TVApp.DAOSQL
{
    public class Context: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=tvapp_db.db");
        }

        public virtual DbSet<IProducer> producers { get; set; }
        public virtual DbSet<ITV> TVs { get; set; }

    }
}