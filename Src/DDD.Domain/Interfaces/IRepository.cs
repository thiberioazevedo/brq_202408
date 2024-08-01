using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Domain.Models;

namespace DDD.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetBy(long id, bool detach = false);
        IQueryable<TEntity> GetAll(Dictionary<string, List<string>> queryParameterDictionary = null);
        //IQueryable<TEntity> GetAll(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAllSoftDeleted();
        void Update(TEntity obj);
        void Remove(long id);
        void Remove(TEntity resposta);
        int SaveChanges();
    }
}
