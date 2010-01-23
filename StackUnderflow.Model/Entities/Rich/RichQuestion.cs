using System.Collections.Generic;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Model.Entities.Rich
{
    public class RichQuestion
    {
        public RichQuestion(Question question, int votes, VoteType? currentUsersVotes, List<RichAnswer> answers, int answerCount)
        {
            Question = question;
            Votes = votes;
            CurrentUsersVote = currentUsersVotes;
            Answers = answers;
            AnswerCount = answerCount;
        }

        public Question Question { get; private set; }
        public List<RichAnswer> Answers { get; private set; }
        public int Votes { get; private set; }
        public VoteType? CurrentUsersVote { get; private set; }

        /// <summary>
        /// If Answers is null, this should contain the answer count.
        /// </summary>
        public int AnswerCount {get; private set;}
    }
}
