using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Model.Entities.Rich
{
    public class RichAnswer
    {
        public RichAnswer(Answer answer, VoteType? currentUsersVote)
        {
            Answer = answer;
            CurrentUserVote = currentUsersVote;
        }

        public Answer Answer { get; set; }
        public VoteType? CurrentUserVote { get; private set; }
    }
}