using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoWorking.Domain.Entities.Base;

namespace CoWorking.Domain.BusinessConracts.Base
{
    public interface IManager<TEntity> where TEntity : BaseEntity
    {
        bool Create(TEntity entity);
        bool Update(TEntity entity);
        bool Remove(TEntity entity);
        IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);
        TEntity Get(params object[] keys);
        IEnumerable<TEntity> GetAll();
    }
}
