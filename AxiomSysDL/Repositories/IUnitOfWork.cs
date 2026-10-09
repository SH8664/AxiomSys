namespace AxiomSysDL.Repositories
{
    public interface IUnitOfWork<T>
    {
        IRepository<T> GetRepository<T>() where T : class;
        int Commit();
    }
}
