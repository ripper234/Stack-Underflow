using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Persistence.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository()
        {
        }
    }
}