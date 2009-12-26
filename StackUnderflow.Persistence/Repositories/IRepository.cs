namespace StackUnderflow.Persistence.Repositories
{
    public interface IRepository<T>
    {
        void Save(T entity);
        T GetById(int id);
    }
}