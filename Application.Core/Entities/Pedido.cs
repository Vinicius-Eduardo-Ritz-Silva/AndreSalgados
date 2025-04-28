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
        public bool Pago { get; set; }

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
    }
}
