using NHibernate;
using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}