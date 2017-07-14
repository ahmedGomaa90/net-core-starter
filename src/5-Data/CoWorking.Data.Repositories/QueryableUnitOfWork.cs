using System;
using Microsoft.EntityFrameworkCore;
using CoWorking.Data.Context;

namespace CoWorking.Data.Repositories
{
    public class QueryableUnitOfWork : IQuerableUnitOfWork, IDisposable
    {
        private static DbContext _context;

        public QueryableUnitOfWork()
        {
            _context = new CoWorkingContext();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public void SetModify(object obj)
        {
            //If object with the same id in the OSM then should be detached
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void SetRemoved(object obj)
        {
            //If object with the same id in the OSM then should be detached
            _context.Entry(obj).State = EntityState.Deleted;
        }


        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
