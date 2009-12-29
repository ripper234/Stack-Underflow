#region

using Castle.ActiveRecord;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public abstract class RepositoryBase<T> where T : ActiveRecordBase<T>
    {
        public T GetById(int id)
        {
            return ActiveRecordBase<T>.Find(id);
        }

        public void Save(T entity)
        {
            entity.Save();
        }
    }
}