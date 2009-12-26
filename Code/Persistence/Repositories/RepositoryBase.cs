using System;
using NHibernate;
using NHibernate.Linq;

namespace StackUnderflow.Persistence.Repositories
{
    public abstract class RepositoryBase<T>
    {
        protected readonly ISessionFactory SessionFactory;

        protected RepositoryBase(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        protected void RunNonQuery(Action<ISession> query)
        {
            using (var session = SessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                query(session);
                tx.Commit();
            }
        }

        protected T RunQuery(Func<ISession, T> query)
        {
            using (var session = SessionFactory.OpenSession())
            {
                return query(session);
            }
        }

        public T GetById(int id)
        {
            return RunQuery(session => session.Load<T>(id));
        }

        public void Save(T entity)
        {
            RunNonQuery(session => session.Save(entity));
        }
    }
}