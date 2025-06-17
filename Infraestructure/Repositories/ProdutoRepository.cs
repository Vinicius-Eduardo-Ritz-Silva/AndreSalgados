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
    public class ProdutoRepository : MainRepository<Produto>, IProdutoRepository
    {
        private readonly VrContext _context;

        public ProdutoRepository(VrContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ResultadoDTO> ValidarProduto(Produto produto)
        {
            try
            {
                if (produto.Preco < 0)
                    return new ResultadoDTO
                    {
                        Sucesso = false,
                        Mensagem = "O preço deve ser maior que zero!"
                    };

                if (produto.Quantidade < 0)
                    return new ResultadoDTO
                    {
                        Sucesso = false,
                        Mensagem = "A quantidade deve ser maior que zero!"
                    };

                return new ResultadoDTO
                {
                    Sucesso = true
                };
            }
            catch (Exception ex)
            {
                return new ResultadoDTO
                {
                    Sucesso = false,
                    Mensagem = "Erro ao validar o produto!"
                };
            }
        }
    }
}
