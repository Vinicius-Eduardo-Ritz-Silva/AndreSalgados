using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VrContext _context;

        public PedidoRepository(VrContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> Get()
        {
            try
            {
                var pedidos = await _context.Pedidos
                    .Where(c => c.Ativo == true).OrderBy(c => c.Cliente.Nome).ToListAsync();

                return pedidos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
