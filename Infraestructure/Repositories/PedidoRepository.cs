using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Azure;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class PedidoRepository : MainRepository<Pedido>, IPedidoRepository
    {
        private readonly VrContext _context;

        public PedidoRepository(VrContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetPedidos()
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

        public async Task<Pedido> GetPedidoById(Guid Id)
        {
            try
            {
                var pedido = await _context.Pedidos.FirstOrDefaultAsync(c => c.Id == Id);

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Pedido GetPedidoByProdutoPedido(Guid ProdutoPedidoId)
        {
            try
            {
                var produtoPedido = _context.ProdutosPedidos.FirstOrDefault(pp => pp.Id == ProdutoPedidoId);
                var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == produtoPedido.PedidoId);

                return pedido;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SalvarPedido(Pedido pedido)
        {
            try
            {
                var pedidoExistente = _context.Pedidos.AsNoTracking()
                    .FirstOrDefault(p => p.Id == pedido.Id);

                if (pedidoExistente == null)
                {
                    _context.Add(pedido);
                }
                else
                {
                    pedido.Inclusao = pedidoExistente.Inclusao;
                    pedido.Valor = pedidoExistente.Valor;
                    pedido.Quantidade = pedidoExistente.Quantidade;

                    _context.Update(pedido);
                }
                    

                _context.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
