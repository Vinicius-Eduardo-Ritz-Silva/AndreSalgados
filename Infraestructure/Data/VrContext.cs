using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Core.Entities;

namespace Infraestructure.Data
{
    public class VrContext : DbContext
    {
        public VrContext(DbContextOptions<VrContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cobranca> Cobrancaes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoPedido> ProdutosPedidos { get; set; }
        public DbSet<RelatorioVenda> RelatoriosVendas { get; set; }
        public DbSet<TipoProduto> TiposProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configurar o resto depois
        }

        private void ConfigureMainModel(ModelBuilder modelBuilder)
        {
            //Configurar depois
        }
    }
}
