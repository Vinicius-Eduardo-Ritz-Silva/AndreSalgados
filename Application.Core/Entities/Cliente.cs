using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Entities
{
    [Table("VR_CLIENTE")]
    public class Cliente : MainModel
    {
        #region -> Propriedades

        [Column("NM_NOMECLIE")]
        public string Nome { get; set; }

        [Column("NR_NUMECLIE")]
        public string Numero { get; set; }

        [Column("GN_ENDECLIE")]
        public string Endereco { get; set; }

        #endregion
    }
}
