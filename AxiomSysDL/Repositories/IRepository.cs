namespace AxiomSysDL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T? Find(params object[] keys);
        void Update(T entity);
        void Add(T entity);
        void Remove(T entity);
    }

}
