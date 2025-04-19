using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Entities
{
    [Table("VR_VENDAS_RELATORIO")]
    public class RelatorioVenda : MainModel
    {
        #region Propriedades

        [Column("NR_VALOVEND")]
        public decimal Valor { get; set; }

        [Column("FL_VENDGANH")]
        public bool VendaGanha { get; set; }

        #endregion

        #region Relacionamentos

        [Column("ID_CLIE")]
        public Guid ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        #endregion
    }
}
