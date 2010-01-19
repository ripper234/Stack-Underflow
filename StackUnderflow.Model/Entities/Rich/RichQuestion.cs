using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Model.Entities.Rich
{
    public class RichQuestion
    {
        public RichQuestion(Question question, int votes, VoteType? currentUsersVotes)
        {
            Question = question;
            Votes = votes;
            CurrentUsersVote = currentUsersVotes;
        }

        public Question Question { get; private set; }
        public int Votes { get; private set; }
        public VoteType? CurrentUsersVote { get; private set; }
    }
}
