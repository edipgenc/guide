using Guide.Book.Data.Repositories.Interfaces;
using Guide.Book.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Book.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationContext _dbContext;
        
        private DbSet<TEntity> _entities;
        protected virtual DbSet<TEntity> Entities => _entities ?? (_entities = _dbContext.Set<TEntity>());
        public GenericRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }
        public IEnumerable<TEntity> GetSql(string sql)
        {
            return Entities.FromSqlRaw(sql).AsNoTracking();
        }
        public async Task<string> Create(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.Created = DateTime.Now;
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id.ToString();
        }

        public async Task<bool> Delete(string id)
        {
            var entitiy = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entitiy);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(string id)
        {
            Guid objGuid = Guid.Empty;
            objGuid = Guid.Parse(id);
            return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == objGuid);
        }

        public async Task<bool> Update(TEntity entity)
        {
            entity.Updated = DateTime.Now;
            _dbContext.Set<TEntity>().Update(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
