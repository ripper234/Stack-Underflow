#region

using Castle.ActiveRecord;
using NHibernate;
using StackUnderflow.Model.Entities;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public override void Save(User user)
        {
            if (user.Reputation <= 0)
                user.Reputation = 1;

            ActiveRecordMediator<User>.Save(user);
            base.Save(user);
        }
    }
}