using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Entities
{
    [Table("VR_PRODUTO")]
    public class Produto : MainModel
    {
        #region Propriedades

        [Column("NM_NOMEPROD")]
        public string Descricao1 { get; set; }

        [Column("GN_DESCPROD")]
        public string Descricao2 { get; set; }

        [Column("NR_PRECPROD")]
        public decimal Preco { get; set; }

        [Column("NR_QUANPROD")]
        public int Quantidade { get; set; }

        #endregion

        #region Relacionamentos

        [Column("ID_TIPO")]
        public Guid? TipoId { get; set; }

        [ForeignKey("TipoId")]
        public virtual TipoProduto Tipo { get; set; }

        #endregion
    }
}
