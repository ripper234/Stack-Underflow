namespace StackUnderflow.Persistence.Repositories
{
    public interface IRepository<T>
    {
        void Save(T entityt);
        T GetById(int id);
    }
}