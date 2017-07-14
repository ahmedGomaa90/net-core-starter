using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CoWorking.Domain.Entities.Base;

namespace CoWorking.Domain.DataContracts.Base
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<TEntity> GetAll();
        TEntity Get(params object[] key);
        void Add(TEntity entityToAdd);
        void Modify(TEntity entityToModify);
        void Remove(TEntity entityToRemove);
        IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter);
    }
}
