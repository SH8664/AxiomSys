using Microsoft.EntityFrameworkCore;

namespace AxiomSysDL.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _db;
        public UnitOfWork(TContext db) => _db = db;
        public IRepository<T> GetRepository<T>() where T : class => new Repository<T>(_db);
        public int Commit() => _db.SaveChanges();
    }
}
