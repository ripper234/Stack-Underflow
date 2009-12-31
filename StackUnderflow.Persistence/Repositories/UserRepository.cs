#region

using NHibernate;
using StackUnderflow.Persistence.Entities;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}