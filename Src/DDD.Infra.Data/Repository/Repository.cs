using System;
using System.Collections.Generic;
using System.Linq;
using DDD.Domain.Core.Models;
using DDD.Domain.Interfaces;
using DDD.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DDD.Infra.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityAudit
    {
        public ApplicationDbContext Db { get; internal set; }
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            obj.Criacao = DateTime.Now;
            DbSet.Add(obj);
        }

        public virtual TEntity GetBy(long id, bool detach = false)
        {
            //return DbSet.Find(id);

            var entity = GetByIdQuery(id).FirstOrDefault();

            if (entity != null && detach)
                Db.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public virtual IQueryable<TEntity> GetByIdQuery(long id)
        {
            return DbSet.Where(i => i.Id == id);

        }

        public virtual IQueryable<TEntity> GetAll(Dictionary<string, List<string>> queryParameterDictionary = null)
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> GetAllSoftDeleted()
        {
            return DbSet.IgnoreQueryFilters()
                .Where(e => EF.Property<bool>(e, "IsDeleted") == true);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(long id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public virtual void Remove(TEntity entity)
        {
            entity.Excluido = true;
            entity.Exclusao = DateTime.Now;
            Update(entity);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        //private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        //{
        //    return SpecificationEvaluator<TEntity>.GetQuery(DbSet.AsQueryable(), spec);
        //}
    }
}
