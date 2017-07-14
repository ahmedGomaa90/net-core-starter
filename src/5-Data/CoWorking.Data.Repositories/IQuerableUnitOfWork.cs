using Microsoft.EntityFrameworkCore;
using CoWorking.Domain.DataContracts;

namespace CoWorking.Data.Repositories
{
    public interface IQuerableUnitOfWork : IUnitOfWork
    {
        DbSet<TEntity> GetSet<TEntity>() where TEntity : class;
        void SetModify(object obj);
        void SetRemoved(object obj);

    }
}
