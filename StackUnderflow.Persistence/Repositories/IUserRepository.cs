#region

using StackUnderflow.Model.Entities;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByOpenId(string openId);
    }
}