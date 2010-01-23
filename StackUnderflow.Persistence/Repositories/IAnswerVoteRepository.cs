using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public interface IAnswerVoteRepository : IPostVoteRepository<VoteOnAnswer, Answer>
    {
    }
}