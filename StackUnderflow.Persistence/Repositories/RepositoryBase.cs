#region

using System;
using Castle.ActiveRecord;
using NHibernate;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public abstract class RepositoryBase<T> where T : ActiveRecordBase<T>
    {
        protected RepositoryBase(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        protected ISessionFactory SessionFactory
        {
            get; private set;
        }

        public T GetById(int id)
        {
            return ActiveRecordBase<T>.Find(id);
        }

        public void Save(T entity)
        {
            ValidateOnSave(entity);
            entity.Save();
        }

        protected virtual void ValidateOnSave(T entity)
        {
        }
    }
}