using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Entities;
using Application.Core.Interfaces.Repositories;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class MainRepository<T> : IMainRepository<T> where T : class
    {
        private readonly VrContext _context;

        public MainRepository(VrContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> lambda)
        {
            try
            {
                var entity = await _context.Set<T>()
                    .Where(lambda).AsNoTracking().ToListAsync();
                    //.Where(c => c.Ativo == true).OrderBy(c => c.Cliente.Nome).ToListAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<IEnumerable<T>> GetAtivos()
        //{
        //    var entity = await _context.Set<T>().AsNoTracking().ToListAsync(); ;

        //    Get(t=>t.Ativo == true);
        //}
    }
}
