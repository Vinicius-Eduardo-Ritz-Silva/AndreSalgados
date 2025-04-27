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
    public class MainRepository<T> : IMainRepository<T> where T : MainModel
    {
        private readonly VrContext _context;
        protected readonly DbSet<T> _dbSet;

        public MainRepository(VrContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> Get()
        {
            try
            {
                var entities = await _dbSet
                    .Where(c => c.Ativo)
                    .AsNoTracking()
                    .ToListAsync();

                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> GetWithInclude(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var entities = _dbSet.Where(c => c.Ativo).AsNoTracking();

                foreach (var include in includes)
                {
                    entities = entities.Include(include);
                }

                return await entities.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetById(Guid Id)
        {
            try
            {
                var entity = await _dbSet
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == Id);

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InsertOrReplace(T Entity)
        {
            try
            {
                var oldEntity = await _dbSet
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == Entity.Id);

                if (oldEntity != null)
                {
                    Entity.Inclusao = oldEntity.Inclusao;

                    _dbSet.Update(Entity);
                }
                else
                {
                    await _dbSet.AddAsync(Entity);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Excluir(Guid Id)
        {
            try
            {
                var entity = await _dbSet
                    .FirstOrDefaultAsync(c => c.Id == Id);

                entity.Ativo = false;

                _dbSet.Update(entity);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
