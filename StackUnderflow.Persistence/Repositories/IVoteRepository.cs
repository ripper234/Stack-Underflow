using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Persistence.Repositories
{
    public interface IVoteRepository : IRepository<VoteOnQuestion>
    {
        void AddVote(User user, Question question, VoteType voteType);
        VoteCount GetVoteCount(int questionId);
        VoteOnQuestion GetVote(User user, Question question);
    }
}
