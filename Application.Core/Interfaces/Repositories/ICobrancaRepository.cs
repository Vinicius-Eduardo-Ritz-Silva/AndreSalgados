using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;

namespace Application.Core.Interfaces.Repositories
{
    public interface ICobrancaRepository : IMainRepository<Cobranca>
    {
        public bool GerarCobranca(Pedido pedido);

        public bool QuitarCobranca(Guid id);

        public bool DefinirDataCobranca(Guid id, DateTime dataCobranca);

        public bool MarcarComoPerdida(Guid id);
    }
}
