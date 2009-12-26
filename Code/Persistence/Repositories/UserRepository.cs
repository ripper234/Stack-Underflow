using NHibernate;
using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public UserRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        #region IUserRepository Members

        public void Save(User user)
        {
            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                session.Save(user);
                tx.Commit();
            }
        }

        #endregion
    }
}