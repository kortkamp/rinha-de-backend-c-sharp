using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Rinha.Data.Mapping;
using Rinha.Entities;

namespace Rinha.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
         : base(options)
        {
        }
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfiguration(new PessoaMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}