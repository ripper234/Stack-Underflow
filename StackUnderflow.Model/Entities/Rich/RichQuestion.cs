using System.Collections.Generic;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Model.Entities.Rich
{
    public class RichQuestion
    {
        public RichQuestion(Question question, int votes, VoteType? currentUsersVotes, List<Answer> answers)
        {
            Question = question;
            Votes = votes;
            CurrentUsersVote = currentUsersVotes;
            Answers = answers;
        }

        public Question Question { get; private set; }
        public List<Answer> Answers { get; private set; }
        public int Votes { get; private set; }
        public VoteType? CurrentUsersVote { get; private set; }
    }
}
