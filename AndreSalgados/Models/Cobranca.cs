using System.ComponentModel.DataAnnotations.Schema;

namespace AndreSalgados.Models
{
    [Table("VR_COBRANCA")]
    public class Cobranca : MainModel
    {
        #region Propriedades

        [Column("DT_DATACOBR")]
        public DateTime DataCobranca { get; set; }

        [Column("NR_VALOCOBR")]
        public decimal Valor { get; set; }

        [Column("NR_VALODESC")]
        public decimal? Desconto { get; set; }

        [Column("FL_DESCPORC")]
        public bool DescontoPorcento { get; set; }

        [Column("FL_COBRPERD")]
        public bool CobrancaPerdida { get; set; }

        #endregion

        #region Relacionamentos

        [Column("ID_CLIE")]
        public Guid ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        #endregion

        [NotMapped]
        public decimal ValorFinal 
        { 
            get
            {
                if (!DescontoPorcento)
                    return Valor - (Desconto ?? 0);

                else
                    return Valor * ((Desconto ?? 100) / 100m);
            }
        }
    }
}
