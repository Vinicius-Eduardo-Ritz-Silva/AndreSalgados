using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.DTOs;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class RelatorioVendaRepository : IRelatorioVendaRepository
    {
        private readonly VrContext _context;

        public RelatorioVendaRepository(VrContext context) 
        {
            _context = context;
        }
        public async Task<ProdutosMiasPedidosDTO> ProdutosMaisPedidos()
        {
            try
            {
                var query = $@"";

                var retorno = new ProdutosMiasPedidosDTO();

                return retorno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
