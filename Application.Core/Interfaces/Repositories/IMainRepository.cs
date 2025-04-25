using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;

namespace Application.Core.Interfaces.Repositories
{
    public interface IMainRepository<T> where T : MainModel
    {
        public Task<IEnumerable<T>> Get();

        public Task<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includes);

        public Task<T> GetById(Guid Id);

        public Task<bool> InsertOrReplace(T Entity);

        public Task<bool> Excluir(Guid Id);
    }
}
