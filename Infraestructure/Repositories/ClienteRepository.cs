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
    public class ClienteRepository : MainRepository<Cliente>, IClienteRepository
    {
        private readonly VrContext _context;

        public ClienteRepository(VrContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ResultadoDTO> ValidarNumero(Cliente cliente)
        {
            try
            {
                var retorno = await _context.Clientes
                    .AnyAsync(c => c.Numero == cliente.Numero && c.Ativo && c.Id != cliente.Id);

                if (!retorno)
                    return new ResultadoDTO
                    {
                        Sucesso = true
                    };
                else
                    return new ResultadoDTO
                    {
                        Sucesso = false,
                        Mensagem = "Esse número já está registrado!"
                    };
            }
            catch (Exception)
            {
                return new ResultadoDTO
                {
                    Sucesso = false,
                    Mensagem = "Houve algum erro ao validar o número!"
                };
            }
        }
    }
}
