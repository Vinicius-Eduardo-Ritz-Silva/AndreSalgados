using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.DTOs;
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

        public ResultadoDTO AdicionarProdutoPedido(Guid PedidoId, Guid ProdutoId, int Quantidade)
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

                var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == PedidoId);

                pedido.Quantidade += produtoPedido.Quantidade;
                pedido.Valor += produtoPedido.SubTotal;

                _context.Update(pedido);

                _context.SaveChanges();

                return new ResultadoDTO
                {
                    Sucesso = true,
                    Dados = pedido
                };
            }
            catch
            {
                return new ResultadoDTO
                {
                    Sucesso = false
                };
            }
        }

        public bool AtualizarQuantidadeProdutoPedido(Guid Id, int Quantidade)
        {
            try
            {
                var produtoPedido = _context.ProdutosPedidos.FirstOrDefault(pp => pp.Id == Id);
                var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == produtoPedido.PedidoId);

                if (produtoPedido.Quantidade > Quantidade)
                {
                    pedido.Quantidade -= (produtoPedido.Quantidade - Quantidade);
                    pedido.Valor -= (produtoPedido.SubTotal - (Quantidade * produtoPedido.Valor));
                }
                else if (produtoPedido.Quantidade < Quantidade)
                {
                    pedido.Quantidade += (Quantidade - produtoPedido.Quantidade);
                    pedido.Valor += ((Quantidade * produtoPedido.Valor) - produtoPedido.SubTotal);
                }

                produtoPedido.Quantidade = Quantidade;

                _context.Update(produtoPedido);
                _context.Update(pedido);

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
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

        public bool ProdutoExisteNoPedido(Guid PedidoId, Guid ProdutoId)
        {
            try
            {
                var retorno = _context.ProdutosPedidos
                .Any(pp => pp.PedidoId == PedidoId && pp.ProdutoId == ProdutoId && pp.Ativo);

                return retorno;
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

                var pedido = _context.Pedidos.FirstOrDefault(p => p.Id == produtoPedido.PedidoId);

                pedido.Quantidade -= produtoPedido.Quantidade;
                pedido.Valor -= produtoPedido.SubTotal;

                _context.Update(pedido);

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ValidarQuantidadeProdutoPedido(int quantidade)
        {
            try
            {
                if (quantidade < 1)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
