using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.DTOs;
using Application.Core.Entities;

namespace Application.Core.Interfaces.Repositories
{
    public interface IProdutoPedidoRepository
    {
        public ResultadoDTO AdicionarProdutoPedido(Guid PedidoId, Guid ProdutoId, int Quantidade);

        public IEnumerable<ProdutoPedido> GetProdutoByPedido(Guid PedidoId);

        public bool RemoverProdutoPedido(Guid Id);

        public bool AtualizarQuantidadeProdutoPedido(Guid Id, int Quantidade);

        public bool ProdutoExisteNoPedido(Guid PedidoId, Guid ProdutoId);
    }
}
