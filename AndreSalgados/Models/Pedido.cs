namespace AndreSalgados.Models
{
    public class Pedido : MainModel
    {
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }

        public Guid ClienteId { get; set; }
        public virtual Clientes Cliente { get; set; }
    }
}
