using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Persistence.Repositories
{
    public interface IUserRepository
    {
        void Save(User user);
    }
}