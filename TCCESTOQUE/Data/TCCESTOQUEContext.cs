using Microsoft.EntityFrameworkCore;
using TCCESTOQUE.Models;

namespace TCCESTOQUE.Data
{
    public class TCCESTOQUEContext : DbContext
    {
        public TCCESTOQUEContext(DbContextOptions<TCCESTOQUEContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //restringe a exclusão de um fornecedor se ele tiver entradas
            modelBuilder.Entity<EntradaModel>()
                .HasOne(p => p.Fornecedor)
                .WithMany(c => c.Entradas)
                .HasForeignKey(p => p.FornecedorId)
                .OnDelete(DeleteBehavior.Restrict);

            //restringe a exlcusão de um cliente quando ele tiver venda 
            modelBuilder.Entity<VendaModel>()
                .HasOne(c => c.Cliente)
                .WithMany(p => p.Venda)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<TCCESTOQUE.Models.VendedorModel> VendedorModel { get; set; }

        public DbSet<TCCESTOQUE.Models.FornecedorModel> FornecedorModel { get; set; }

        public DbSet<TCCESTOQUE.Models.ProdutoModel> ProdutoModel { get; set; }

        public DbSet<TCCESTOQUE.Models.FornecedorEnderecoModel> FornecedorEnderecoModel { get; set; }

        public DbSet<TCCESTOQUE.Models.ClienteModel> ClienteModel { get; set; }

        public DbSet<TCCESTOQUE.Models.VendaModel> VendaModel { get; set; }

        public DbSet<TCCESTOQUE.Models.VendaItensModel> VendaItensModel { get; set; }

        public DbSet<TCCESTOQUE.Models.ClienteEnderecoModel> ClienteEnderecoModel { get; set; }

        public DbSet<TCCESTOQUE.Models.CarrinhoModel> CarrinhoModel { get; set; }

        public DbSet<TCCESTOQUE.Models.EntradaModel> EntradaModel { get; set; }

        public DbSet<TCCESTOQUE.Models.AlterarSenhaModel> AlterarSenhaModel { get; set; }
    }
}
