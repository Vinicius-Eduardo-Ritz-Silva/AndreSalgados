using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

            var isPostgres = Database.ProviderName?.ToLower().Contains("npgsql") == true;
            var isSqlServer = Database.ProviderName?.ToLower().Contains("sqlserver") == true;

            if (isPostgres)
            {
                ConfigureForPostgres(modelBuilder);
            }
            else if (isSqlServer)
            {
                ConfigureForSqlServer(modelBuilder);
            }
        }

        private void ConfigureForPostgres(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(MainModel).IsAssignableFrom(entityType.ClrType))
                    continue;

                var rawTableName = entityType.GetTableName();
                if (string.IsNullOrWhiteSpace(rawTableName))
                    continue;

                var tableName = rawTableName.Replace("VR_", "").Replace("_", "");

                RenameCommonColumns(entityType, tableName);

                var idProperty = entityType.FindProperty(nameof(MainModel.Id));
                if (idProperty != null)
                {
                    // Define a coluna como tipo uuid
                    idProperty.SetColumnType("uuid");

                    // Define que o valor será gerado no banco pelo PostgreSQL
                    idProperty.SetDefaultValueSql("gen_random_uuid()");

                    // Define como chave primária
                    entityType.SetPrimaryKey(idProperty);
                }

                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(Guid) || property.ClrType == typeof(Guid?))
                    {
                        property.SetColumnType("uuid");
                    }
                }
            }
        }

        private void ConfigureForSqlServer(ModelBuilder modelBuilder)
        {

            //Configurar o resto depois
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(MainModel).IsAssignableFrom(entityType.ClrType))
                {
                    // Obtém o nome da tabela (removendo prefixos como "VR_")
                    var tableName = entityType.GetTableName()
                        .Replace("VR_", "").Replace("_", "");

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

        private void RenameCommonColumns(IMutableEntityType entityType, string tableName)
        {
            void SetColumn(string propertyName, string columnName)
            {
                var property = entityType.FindProperty(propertyName);
                if (property != null)
                    property.SetColumnName(columnName);
            }

            SetColumn(nameof(MainModel.Id), $"ID_{tableName}");
            SetColumn(nameof(MainModel.Inclusao), $"DT_INCL{tableName}");
            SetColumn(nameof(MainModel.Alteracao), $"DT_ALTE{tableName}");
            SetColumn(nameof(MainModel.CodigoExterno), $"IE_CODI{tableName}");
            SetColumn(nameof(MainModel.Ativo), $"FL_ATIV{tableName}");
        }

        #endregion
    }
}
