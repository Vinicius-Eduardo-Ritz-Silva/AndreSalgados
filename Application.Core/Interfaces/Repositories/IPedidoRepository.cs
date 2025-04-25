using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;

namespace Application.Core.Interfaces.Repositories
{
    public interface IPedidoRepository : IMainRepository<Pedido>
    {
        public Task<IEnumerable<Pedido>> GetPedidos();

        public Task<Pedido> GetPedidoById(Guid Id);

        public bool SalvarPedido(Pedido pedido);
    }
}
