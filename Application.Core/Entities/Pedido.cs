using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Entities
{
    [Table("VR_PEDIDO")]
    public class Pedido : MainModel
    {
        #region -> Propriedades

        [Column("NR_QUANPEDI")]
        public int Quantidade { get; set; }

        [Column("NR_VALOPEDI")]
        public decimal Valor { get; set; }

        [Column("FL_PEDIPAGO")]
        public PedidoStatus Pago { get; set; }

        [Column("EN_STATCOBR")]
        public CobrancaStatus? Status { get; set; }

        [Column("FL_EDITPEDI")]
        public bool Edicao { get; set; }

        #endregion

        #region -> Relacionamentos

        [Column("ID_CLIE")]
        public Guid ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [Column("ID_COBR")]
        public Guid? CobrancaId { get; set; }

        [ForeignKey("CobrancaId")]
        public virtual Cobranca Cobranca { get; set; }

        #endregion

        #region -> Enum's

        public enum PedidoStatus
        {
            Pendente = 0,
            Pago = 1,
            Perdido = 2,
            Fechado = 3
        }

        public enum CobrancaStatus
        {
            PagoNaHora = 0,
            PagoEmDia = 1,
            PagoComAtraso = 2,
            NaoPago = 3
        }

        #endregion
    }
}
