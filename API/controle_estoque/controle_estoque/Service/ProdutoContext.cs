using controle_estoque.Models;
using Microsoft.EntityFrameworkCore;

namespace controle_estoque.Service
{
    public class ProdutoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "CeDb");
        }

        public DbSet<Produto> Produto { get; set; }
    }
}
