using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoWorking.Domain.BusinessConracts.Base;
using CoWorking.Domain.BusinessConracts.Managers;
using CoWorking.Domain.DataContracts.Repositories;
using CoWorking.Domain.Entities;

namespace CoWorking.Business.Managers
{
    public class UserManager : IManager<User>, IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Create(User entity)
        {
             _userRepository.Add(entity);
             
             return true;
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetByFilter(Expression<Func<User, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public User Get(params object[] keys)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
    }
}
