using Microsoft.EntityFrameworkCore;

namespace AxiomSysDL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _db;
        public Repository(DbContext db) => _db = db;
        public IQueryable<T> GetAll() => _db.Set<T>();
        public T? Find(params object[] keys) => _db.Set<T>().Find(keys);
        public void Update(T e) => _db.Set<T>().Update(e);
        public void Add(T e) => _db.Set<T>().Add(e);
        public void Remove(T e) => _db.Set<T>().Remove(e);


    }
}
