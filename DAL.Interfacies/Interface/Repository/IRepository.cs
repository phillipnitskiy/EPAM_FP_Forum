using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> predicate);
        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}


