using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;

namespace Application.Core.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        public Task<IEnumerable<Cliente>> Get();

        public Task<Cliente> GetClienteById(Guid Id);

        public bool SalvarCliente(Cliente cliente);

        public bool ExcluirCliente(Guid Id);
    }
}
