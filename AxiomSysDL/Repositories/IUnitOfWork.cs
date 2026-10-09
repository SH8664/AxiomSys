namespace AxiomSysDL.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
        int Commit();
    }
}
