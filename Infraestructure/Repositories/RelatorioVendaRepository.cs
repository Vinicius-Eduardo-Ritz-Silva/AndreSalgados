using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.DTOs;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Infraestructure.Repositories
{
    public class RelatorioVendaRepository : IRelatorioVendaRepository
    {
        private readonly VrContext _context;
        private readonly IConfiguration _configuration;

        public RelatorioVendaRepository(VrContext context,
                                        IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IEnumerable<ProdutosMiasPedidosDTO>> ProdutosMaisPedidos()
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                //_hostingEnvironment.IsDevelopment()
                //? _configuration.GetConnectionString("DefaultConnection")
                //: VrCommon.Base64Decode(VrCommon.Base64Decode(_configuration.GetConnectionString("DefaultConnection")));

                var query = $@"SELECT TOP 10
                                    p.NM_NOMEPROD AS Nome,
                                    COUNT(pp.ID_PRODUTO) AS Quantidade
                               FROM 
                                    VR_PRODUTOPEDIDO pp
                               INNER JOIN 
                                    VR_PRODUTO p ON pp.ID_PRODUTO = p.ID_PRODUTO
                               WHERE 
                                    pp.FL_ATIVPRODUTO = 1
                                    AND p.FL_ATIVPEDIPROD = 1
                               GROUP BY
                                    p.NM_NOMEPROD,
                                    p.ID_PRODUTO
                               ORDER BY
                                    Quantidade DESC";

                using (var connection = new SqlConnection(connectionString))
                {
                    var retorno = await connection.QueryAsync<ProdutosMiasPedidosDTO>(query);

                    return retorno;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
