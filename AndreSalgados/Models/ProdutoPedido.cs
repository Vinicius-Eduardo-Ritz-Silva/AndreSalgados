using System.ComponentModel.DataAnnotations.Schema;

namespace AndreSalgados.Models
{
    [Table("VR_PRODUTO_PEDIDO")]
    public class ProdutoPedido : MainModel
    {
        #region Propriedades

        [Column("NR_QUANPEDIPROD")]
        public int Quantidade { get; set; }

        [Column("NR_VALOPEDIPROD")]
        public decimal Valor { get; set; }

        #endregion

        #region Relacionamentos

        [Column("ID_PEDI")]
        public Guid PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        public virtual Pedido Pedido { get; set; }

        [Column("ID_PROD")]
        public Guid ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }

        #endregion
    }
}
