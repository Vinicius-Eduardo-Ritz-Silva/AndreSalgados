using System.ComponentModel.DataAnnotations.Schema;

namespace AndreSalgados.Models
{
    public class MainModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Inclusao { get; set; }
        public DateTime Alteracao { get; set; }
        public int? CodigoExterno { get; set; }
        public bool Ativo { get; set; }

        [NotMapped]
        public string GetDataInclusaoCompleta
        {
            get
            {
                return Inclusao.ToString("dd/MM/yyyy HH:mm");
            }
        }

        [NotMapped]
        public string GetDataAlteracaoCompleta
        {
            get
            {
                return Alteracao.ToString("dd/MM/yyyy HH:mm");
            }
        }
    }
}
