using Guide.Book.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IQueryable<TEntity>> GetAll();
        Task<TEntity> GetById(string id);
        Task<string> Create(TEntity entity);
        Task<bool> Update(TEntity entity);
        Task<bool> Delete(string id);


    }
}
