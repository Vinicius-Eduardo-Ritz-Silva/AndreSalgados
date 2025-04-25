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
    public class ProdutoPedidoRepository : IProdutoPedidoRepository
    {
        private readonly VrContext _context;

        public ProdutoPedidoRepository(VrContext context) 
        {
            _context = context;
        }

        public bool AdicionarProdutoPedido(Guid PedidoId, Guid ProdutoId, int Quantidade)
        {
            try
            {
                var produtoPedido = new ProdutoPedido();

                produtoPedido.Inclusao = DateTime.Now;
                produtoPedido.Alteracao = DateTime.Now;
                produtoPedido.Ativo = true;
                produtoPedido.PedidoId = PedidoId;
                produtoPedido.ProdutoId = ProdutoId;
                produtoPedido.Quantidade = Quantidade;

                produtoPedido.Valor = _context.Produtos
                    .Where(p => p.Id == ProdutoId).AsNoTracking()
                    .Select(p => p.Preco).FirstOrDefault();

                _context.Add(produtoPedido);
                _context.SaveChanges(); //Concertar erro de chave estrangeira

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ProdutoPedido> GetProdutoByPedido(Guid PedidoId)
        {
            try
            {
                var produtosPedidos = _context.ProdutosPedidos
                    .Where(pp => pp.PedidoId == PedidoId && pp.Ativo)
                    .Include(pp => pp.Produto)
                    .AsNoTracking()
                    .ToList();

                return produtosPedidos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoverProdutoPedido(Guid Id)
        {
            try
            {
                var produtoPedido = _context.ProdutosPedidos.FirstOrDefault(pp => pp.Id == Id);

                produtoPedido.Ativo = false;

                _context.Update(produtoPedido);

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
