using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;

namespace Application.Core.Interfaces.Repositories
{
    public interface IMainRepository<T> where T : class
    {
        public Task<IEnumerable<T>> Get(Expression<Func<T, bool>> lambda);

        //public Task<IEnumerable<T>> GetAtivos();
    }
}
