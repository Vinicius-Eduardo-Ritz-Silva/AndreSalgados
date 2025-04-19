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

        #region DbSets

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cobranca> Cobrancaes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoPedido> ProdutosPedidos { get; set; }
        public DbSet<RelatorioVenda> RelatoriosVendas { get; set; }
        public DbSet<TipoProduto> TiposProdutos { get; set; }

        #endregion

        #region Configuracoes

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configurar o resto depois
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(MainModel).IsAssignableFrom(entityType.ClrType))
                {
                    // Obtém o nome da tabela (removendo prefixos como "VR_")
                    var tableName = entityType.GetTableName()
                        .Replace("VR_", "").Replace("_","");

                    // Configura os campos herdados
                    entityType.FindProperty(nameof(MainModel.Id))?
                        .SetColumnName($"ID_{tableName}");

                    entityType.FindProperty(nameof(MainModel.Inclusao))?
                        .SetColumnName($"DT_INCL{tableName}");

                    entityType.FindProperty(nameof(MainModel.Alteracao))?
                        .SetColumnName($"DT_ALTE{tableName}");

                    entityType.FindProperty(nameof(MainModel.CodigoExterno))?
                        .SetColumnName($"IE_CODI{tableName}");

                    entityType.FindProperty(nameof(MainModel.Ativo))?
                        .SetColumnName($"FL_ATIV{tableName}");
                }
            }
        }

        #endregion
    }
}
