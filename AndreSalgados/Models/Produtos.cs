namespace AndreSalgados.Models
{
    public class Produtos : MainModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public Guid TipoId { get; set; }
        public virtual TipoProduto Tipo { get; set; }
    }
}
