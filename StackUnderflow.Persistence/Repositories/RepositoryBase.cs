#region

using Castle.ActiveRecord;
using NHibernate;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected RepositoryBase(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        protected ISessionFactory SessionFactory
        {
            get; private set;
        }

        public virtual T GetById(int id)
        {
            return ActiveRecordBase<T>.Find(id);
        }

        /// <summary>
        /// This adds (saves) a new record.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Save(T entity) 
        {
            ActiveRecordMediator<T>.Save(entity);
        }
    }
}