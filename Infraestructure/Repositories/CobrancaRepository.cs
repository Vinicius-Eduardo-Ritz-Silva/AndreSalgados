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
    public class CobrancaRepository : ICobrancaRepository
    {
        private readonly VrContext _context;

        public CobrancaRepository(VrContext context) 
        {
            _context = context;
        }

        public bool GerarCobranca(Pedido pedido)
        {
            try
            {
                var cliente = _context.Clientes.FirstOrDefault(c => c.Id == pedido.ClienteId);
                var cobrancaExistente = _context.Cobrancaes.FirstOrDefault(co => co.ClienteId == cliente.Id);

                if (cobrancaExistente == null)
                {
                    var cobrancaNova = new Cobranca();

                    cobrancaNova.ClienteId = cliente.Id;
                    cobrancaNova.Valor = pedido.Valor;

                    pedido.CobrancaId = cobrancaNova.Id;

                    _context.Add(cobrancaNova);
                    _context.Update(pedido);
                }
                else
                {
                    pedido.CobrancaId = cobrancaExistente.Id;

                    cobrancaExistente.Valor += pedido.Valor;

                    _context.Update(cobrancaExistente);
                    _context.Update(pedido);
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
