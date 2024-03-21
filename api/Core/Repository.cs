using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace api.Core
{
    public class Repository<TEntity, TContext>: IRepository<TEntity>
    where TEntity:class

    where TContext : DbContext
    {
        readonly TContext _context;
        readonly DbSet<TEntity> _entities;

        public Repository(TContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }


        public IQueryable<TEntity> GetAll()
        {
            return _entities;
        }

        public async Task<IList<TEntity>> GetAllBindAsync()
        {
            return await _entities.ToListAsync();
        }

        public virtual Task<PaginatedList<TEntity>> GetPagedAsync(int pageNumber, string sortOrder, string currentFilter, string searchString)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity?> GetByIdAsync(params object[] id)
        {
            return await _entities.FindAsync(id);
        }

        public virtual async Task<TEntity?> CreateAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0 ? entity : null;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            if(!_entities.Contains(entity)) return false;

            _entities.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteByIdAsync(object[] id)
        {
            TEntity? entity = await _entities.FindAsync(id);
            if(entity == null) return false;
            _entities.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            if(!_entities.Contains(entity)) return false;
            _entities.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public virtual Task<bool> Exists(params object[] id)
        {
            throw new NotImplementedException();
        }
    }
}