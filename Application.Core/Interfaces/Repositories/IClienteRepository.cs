using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.DTOs;
using Application.Core.Entities;

namespace Application.Core.Interfaces.Repositories
{
    public interface IClienteRepository : IMainRepository<Cliente>
    {
        public Task<ResultadoDTO> ValidarNumero(string numero);
    }
}
