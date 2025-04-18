using System.ComponentModel.DataAnnotations.Schema;

namespace AndreSalgados.Models
{
    [Table("VR_PEDIDO")]
    public class Pedido : MainModel
    {
        [Column("NR_QUANPEDI")]
        public int Quantidade { get; set; }
        [Column("NR_VALOPEDI")]
        public decimal Valor { get; set; }

        [Column("ID_CLIE")]
        public Guid ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }
    }
}
