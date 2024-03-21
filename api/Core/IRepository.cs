using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Core
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        Task<IList<TEntity>> GetAllBindAsync();

        Task<PaginatedList<TEntity>> GetPagedAsync(int pageNumber, string sortOrder, string currentFilter, string searchString);

        Task<TEntity?> GetByIdAsync(params object[] id);

        Task<TEntity?> CreateAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> DeleteByIdAsync(object[] id);

        Task<bool> Exists(object[] id);
     }
}