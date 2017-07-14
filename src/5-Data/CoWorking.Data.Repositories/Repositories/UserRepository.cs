using CoWorking.Data.Repositories.Base;
using CoWorking.Domain.DataContracts.Repositories;
using CoWorking.Domain.Entities;

namespace CoWorking.Data.Repositories.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IQuerableUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
