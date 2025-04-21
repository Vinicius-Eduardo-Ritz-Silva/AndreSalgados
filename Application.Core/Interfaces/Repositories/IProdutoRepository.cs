using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;

namespace Application.Core.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        public Task<IEnumerable<Produto>> Get();

        public Task<Produto> GetProdutoById(Guid Id);

        public bool SalvarProduto(Produto produto);

        public bool ExcluirProduto(Guid Id);
    }
}
