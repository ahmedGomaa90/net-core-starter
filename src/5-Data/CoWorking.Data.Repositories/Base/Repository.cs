using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CoWorking.Domain.DataContracts;
using CoWorking.Domain.DataContracts.Base;
using CoWorking.Domain.Entities.Base;

namespace CoWorking.Data.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly IQuerableUnitOfWork _unitOfWork;

        private DbSet<TEntity> GetSet()
        {
            if (UnitOfWork == null)
                throw new ArgumentNullException(nameof(UnitOfWork));

            return _unitOfWork.GetSet<TEntity>();
        }

        public Repository(IQuerableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork => _unitOfWork;

        public IQueryable<TEntity> GetAll()
        {
            return GetSet();
        }

        public TEntity Get(params object[] key)
        {

            return GetSet().Find(key);
        }

        public void Add(TEntity entityToAdd)
        {
            if (entityToAdd == null)
                throw new ArgumentNullException(nameof(entityToAdd));
            GetSet().Add(entityToAdd);
        }

        public void Modify(TEntity entityToModify)
        {
            if (entityToModify == null)
                throw new ArgumentNullException(nameof(entityToModify));
            _unitOfWork.SetModify(entityToModify);
        }

        public void Remove(TEntity entityToRemove)
        {
            if (entityToRemove == null)
                throw new ArgumentNullException(nameof(entityToRemove));
            _unitOfWork.SetRemoved(entityToRemove);
        }

        public IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter)
        {

            //checking query arguments
            if (filter == null)
            {
                var err = new ArgumentNullException(nameof(filter), "Filter Cannot Be null");
                
                throw err;
            }

            //Create IDbSet and perform query
            return GetSet().Where(filter).ToList();
        }

    }

}
