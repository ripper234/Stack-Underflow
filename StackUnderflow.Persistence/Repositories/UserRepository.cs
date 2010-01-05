#region

using System;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
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

            if (user.SignupDate == default(DateTime))
                throw new Exception("Missing signup date");

            if (user.OpenId == null)
                throw new Exception("Missing Open ID for user " + user);

            ActiveRecordMediator<User>.Save(user);
            base.Save(user);
        }

        public User FindByOpenId(string openId)
        {
            return ActiveRecordMediator<User>.FindOne(Restrictions.Eq("OpenId", openId));
        }
    }
}