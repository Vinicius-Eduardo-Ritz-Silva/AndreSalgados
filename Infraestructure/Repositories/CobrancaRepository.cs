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
    public class CobrancaRepository : MainRepository<Cobranca>, ICobrancaRepository
    {
        private readonly VrContext _context;

        public CobrancaRepository(VrContext context) : base(context)
        {
            _context = context;
        }

        public bool DefinirDataCobranca(Guid id, DateTime dataCobranca)
        {
            try
            {
                var cobranca = _context.Cobrancaes.FirstOrDefault(co => co.Id == id);

                cobranca.DataCobranca = dataCobranca;
                cobranca.Alteracao = DateTime.Now;

                _context.Update(cobranca);

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
                    cobrancaNova.Inclusao = DateTime.Now;
                    cobrancaNova.Alteracao = DateTime.Now;
                    cobrancaNova.Ativo = true;

                    pedido.CobrancaId = cobrancaNova.Id;

                    _context.Add(cobrancaNova);
                    _context.Update(pedido);
                }
                else
                {
                    pedido.CobrancaId = cobrancaExistente.Id;

                    var pedidosCobrados = _context.Pedidos
                        .Where(p => p.ClienteId == cliente.Id
                               && p.Pago == false && p.Ativo)
                        .AsNoTracking()
                        .ToList();

                    cobrancaExistente.Alteracao = DateTime.Now;
                    cobrancaExistente.Valor = pedidosCobrados.Sum(pc => pc.Valor);

                    _context.Update(cobrancaExistente);
                    //_context.Update(pedido);
                }

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool QuitarCobranca(Guid id)
        {
            try
            {
                var cobranca = _context.Cobrancaes.FirstOrDefault(co => co.Id == id);

                cobranca.Valor = 0;
                cobranca.DataCobranca = null;
                cobranca.Alteracao = DateTime.Now;

                var pedidos = _context.Pedidos.Where(p => p.ClienteId == cobranca.ClienteId && p.Pago == false).ToList();

                foreach (var pedido in pedidos)
                {
                    pedido.Pago = true;

                    _context.Update(pedido);
                }

                _context.Update(cobranca);

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool MarcarComoPerdida(Guid id)
        {
            try
            {
                var cobranca = _context.Cobrancaes.FirstOrDefault(co => co.Id == id);

                cobranca.CobrancaPerdida = true;
                cobranca.Alteracao = DateTime.Now;

                var cliente = _context.Clientes.FirstOrDefault(cl => cl.Id == cobranca.ClienteId);

                cliente.Ativo = false;

                _context.Update(cobranca);
                _context.Update(cliente);

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
