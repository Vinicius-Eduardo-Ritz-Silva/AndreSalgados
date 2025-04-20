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
    public class ClienteRepository : IClienteRepository
    {
        private readonly VrContext _context;

        public ClienteRepository(VrContext context) 
        {
            _context = context;
        }

        public IEnumerable<Cliente> Get()
        {
            try
            {
                var clientes = _context.Clientes.Where(c => c.Ativo == true);

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Cliente GetClienteById(Guid Id)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == Id);

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SalvarCliente(Cliente cliente)
        {
            try
            {
                var clienteExistente = _context.Clientes.FirstOrDefault(c => c.Id == cliente.Id);

                if (clienteExistente != null)
                {
                    cliente.Inclusao = clienteExistente.Inclusao;

                    _context.Update(cliente);
                }
                else
                {
                    _context.Add(cliente);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }    
        }

        public bool ExcluirCliente(Guid Id)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == Id);

                cliente.Ativo = false;

                _context.Update(cliente);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
