using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Core.Entities
{
    [Table("VR_PRODUTO_TIPO")]
    public class TipoProduto : MainModel
    {
        // Ex: Salgado, Doce, Queijo, etc...
        [Column("NM_TIPO")]
        public string Tipo { get; set; }
    }
}
