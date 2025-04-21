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
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly VrContext _context;

        public ProdutoRepository(VrContext context) 
        {
            _context = context;
        }

        public bool ExcluirProduto(Guid Id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(c => c.Id == Id);

                produto.Ativo = false;

                _context.Update(produto);

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Produto>> Get()
        {
            try
            {
                var produtos = await _context.Produtos
                    .Where(c => c.Ativo == true).ToListAsync();

                return produtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetProdutoById(Guid Id)
        {
            try
            {
                var produto = await _context.Produtos.FirstOrDefaultAsync(c => c.Id == Id);

                return produto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SalvarProduto(Produto produto)
        {
            try
            {
                var produtoExistente = _context.Clientes.AsNoTracking().FirstOrDefault(c => c.Id == produto.Id);

                if (produtoExistente != null)
                {
                    produto.Inclusao = produtoExistente.Inclusao;

                    _context.Update(produto);
                }
                else
                {
                    _context.Add(produto);
                }

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
