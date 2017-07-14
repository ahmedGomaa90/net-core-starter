using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoWorking.Domain.BusinessConracts.Base;
using CoWorking.Domain.BusinessConracts.Managers;
using CoWorking.Domain.DataContracts.Repositories;
using CoWorking.Domain.Entities;

namespace CoWorking.Business.Managers
{
    public class DummyManager : IManager<Dummy>, IDummyManager
    {
        private readonly IDummyRepository _dummyRepository;

        public DummyManager(IDummyRepository dummyRepository)
        {
            _dummyRepository = dummyRepository;
        }

        public bool Create(Dummy entity)
        {
             _dummyRepository.Add(entity);
             
             return true;
        }

        public bool Update(Dummy entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Dummy entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dummy> GetByFilter(Expression<Func<Dummy, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Dummy Get(params object[] keys)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dummy> GetAll()
        {
            return _dummyRepository.GetAll();
        }
    }
}
