using CoWorking.Data.Repositories.Base;
using CoWorking.Domain.DataContracts.Repositories;
using CoWorking.Domain.Entities;

namespace CoWorking.Data.Repositories.Repositories
{
    public class DummyRepository : Repository<Dummy>, IDummyRepository
    {
        public DummyRepository(IQuerableUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
