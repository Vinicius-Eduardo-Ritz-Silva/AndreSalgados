using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Data;

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

                _context.Add(produtoPedido);
                //_context.SaveChanges(); //Concertar erro de chave estrangeira

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
